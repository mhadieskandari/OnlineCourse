using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using OnlineCourse.Panel.Utils;
using OnlineCourse.Panel.Utils.Extentions;
using OnlineCourse.Panel.Utils.ViewModels.AccountViewModels;
using SQLitePCL;

namespace OnlineCourse.Panel.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "1")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrentUser _user;

        private readonly IServiceProvider _provider;
        private readonly HistoryService _historyService;
        private readonly MessageService _msgSender;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ApplicationDbContext context, CurrentUser user,HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _user = user;
            _provider = provider;
            _historyService = historyService;
            _msgSender = new MessageService();
            _hostingEnvironment = hostingEnvironment;

        }

        public IActionResult Index()
        {
            //return RedirectToAction("Index","InvoiceList");
            return View();
        }

        public IActionResult Profile()
        {
            var user = _user.GetUser().Result;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserName,Email,Mobile,FullName,Position,OnOff,City,Addrress")] User user, IFormFile image)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var res = new UserUpdate(_provider, _msgSender, _historyService))
                    {
                        var msg = (UpdateUserMessage)res.Update(user);
                        if (msg == UpdateUserMessage.Success)
                        {
                            this.AddNotification(EnumExtention.GetDescription(msg), NotificationType.Success);
                            if (image != null)
                            {

                                Gallery gal;
                                var dbgal = _context.Galleries.Where(g=>g.PublicId==user.Id && g.Kind== (byte)GalleryKind.UserProfile);
                                int count = 0;
                                if (dbgal != null && dbgal.Any())
                                {
                                    gal = dbgal.FirstOrDefault();
                                    gal.POrder = 1;
                                    gal.Ext = Path.GetExtension(image.FileName);
                                    _context.Galleries.Update(gal);
                                    count = _context.SaveChanges();
                                }
                                else
                                {
                                    gal = new Gallery()
                                    {
                                        Kind = (byte)GalleryKind.UserProfile,
                                        PublicId = user.Id,
                                        State = (byte)GeneralState.Disable,
                                        Title = user.FullName,
                                        Ext = Path.GetExtension(image.FileName),
                                        POrder = 1
                                    };
                                    _context.Galleries.Add(gal);
                                    count = _context.SaveChanges();
                                }
                                if (count > 0)
                                {
                                    var filePath = new Uploder(_hostingEnvironment, _historyService).UploadGallery(EncryptDecrypt.GetUrlHash(gal.Id.ToString() + gal.PublicId + gal.Kind), image);

                                    if (string.IsNullOrEmpty(filePath))
                                        this.AddNotification("تصویر باموفقیت ویرایش شد.", NotificationType.Error);
                                }
                                return RedirectToAction("Index");
                            }
                        }
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    this.AddNotification(e.Message, NotificationType.Error);
                    return View();
                }
            }
            return View(user);
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dbUser =await _user.GetUser();
                    if (dbUser != null)
                    {
                        var req = new UserChangePassword(_provider, _msgSender, _historyService).CahngePassword(new ChangePasswordDto() { UserName = dbUser.UserName, Password = model.OldPass, NewPassword = model.NewPass, ConfirmNewPassword = model.ConfirmNewPass, Ip = WebHelper.GetRemoteIP });
                        if (req == (byte)ChangePasswordUserMessage.Success)
                        {

                            this.AddNotification(EnumExtention.GetDescription((ChangePasswordUserMessage)req), NotificationType.Success);
                            var returnUrl = Url.Action(nameof(Profile), "Home", new { area = "Teacher", id = model.UserId });
                            await _user.LogOutAsync();
                            return RedirectToAction("Login", "Account", new { area = "", returnUrl = returnUrl /* , LoginViewModel = new LoginViewModel() { Email = _cUser.GetEmail() }*/});
                        }
                        this.AddNotification(EnumExtention.GetDescription((ChangePasswordUserMessage)req), NotificationType.Error);
                        return RedirectToAction(nameof(Profile), new
                        {
                            id = model.UserId
                        });
                    }

                    this.AddNotification("خطا", NotificationType.Error);
                    return RedirectToAction(nameof(Profile), new
                    {
                        id = model.UserId
                    });

                }
                catch (Exception e)
                {
                    this.AddNotification(e.Message, NotificationType.Error);
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    return RedirectToAction("Error", "Home");
                }
            }
            this.AddNotification("رمز عبور جدید و تکرار آن همخوانی ندارند.", NotificationType.Error);

            return RedirectToAction(nameof(Profile), new
            {
                id = model.UserId
            });
        }


    }
}