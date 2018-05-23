using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using OnlineCourse.Core;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows;
using OnlineCourse.Entity.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Entity;
using OnlineCourse.Panel.Utils.Extentions;
using OnlineCourse.Panel.Utils.ViewModels.Areas.Admin;
using OnlineCourse.Panel.Utils.ViewModels.AccountViewModels;
using OnlineCourse.Panel.Utils;
using AutoMapper;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="10")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _provider;
        private readonly HistoryService _historyService;
        private readonly MessageService _msgSender;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly CurrentUser _cuser;
        private readonly IHtmlLocalizer<SharedResource> _localizer;
        private readonly PublicConfig _config;

        public UsersController(IUnitOfWork unitOfWork, IServiceProvider provider, HistoryService historyService, IHostingEnvironment hostingEnvironment, IMapper mapper, CurrentUser cUser, IHtmlLocalizer<SharedResource> localizer, PublicConfig config)
        {
            _unitOfWork = unitOfWork;
            _provider = provider;
            _historyService = historyService;
            _msgSender = new MessageService();
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _cuser = cUser;
            _localizer = localizer;
            _config = config;
        }

        // GET: Admin/Users
        public IActionResult Index(int? id, string fullname, string username, string email, int page = 1)
        {
            try
            {
                var model = _unitOfWork.Users.GetAll().AsQueryable();
                if (id != null)
                    model = model.Where(m => m.Id == id);
                if (!string.IsNullOrEmpty(fullname))
                    model = model.Where(m => m.FullName.Contains(fullname));
                if (!string.IsNullOrEmpty(username))
                    model = model.Where(m => m.UserName.Contains(username));
                if (!string.IsNullOrEmpty(email))
                    model = model.Where(m => m.Email.Contains(email));


                model = this.AddPagination(model.AsQueryable(), page, _config.GetPageSize());
                return View(model.ToList());
            }
            catch (Exception e)
            {
                this.AddNotification(e.Message, NotificationType.Error);
                _historyService.LogError(e, HistoryErrorType.Middle);
                return View();
            }

        }

        // GET: Admin/Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = _unitOfWork.Users.Get(id.Value);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userr = _mapper.Map<User>(user);
                    var Reg = new UserRegistration(_provider, _msgSender, _historyService);
                    var res = (RegisterUserMessage)Reg.Register(userr);

                    if (res == RegisterUserMessage.Success)
                    {
                        if (user.Image != null)
                        {
                            var gal = new Gallery()
                            {
                                Kind = (byte)GalleryKind.UserProfile,
                                PublicId = user.Id,
                                State = (byte)GeneralState.Disable,
                                Title = user.FullName,
                                Ext = Path.GetExtension(user.Image.FileName),
                            };
                            _unitOfWork.Galleries.Add(gal);
                            var count = _unitOfWork.Complete();
                            if (count > 0)
                            {

                                var filePath = new Uploder(_hostingEnvironment, _historyService).UploadGallery(
                                    EncryptDecrypt.GetUrlHash(gal.Id.ToString() + gal.PublicId + gal.Kind), user.Image);
                                if (!string.IsNullOrEmpty(filePath))
                                    this.AddNotification("خطا درآپلود تصویر پروفایل.", NotificationType.Error);

                                //var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath,
                                //    Path.Combine("uploads", "galleries"));

                                //var filenameandpath = Path.Combine(uploadsRootFolder, Encryption.GetUrlHash(user.Id.ToString() + gal.PublicId + gal.Kind)) + gal.Ext;

                                //if (!Directory.Exists(uploadsRootFolder))
                                //{
                                //    Directory.CreateDirectory(uploadsRootFolder);
                                //}

                                //if (Image.Length > 0)
                                //{
                                //    using (var stream = new FileStream(filenameandpath, FileMode.Create))
                                //    {
                                //        Image.CopyTo(stream);
                                //    }
                                //}
                            }
                        }
                        this.AddNotification(EnumExtention.GetDescription(res),
                            NotificationType.Success);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification(EnumExtention.GetDescription(res),
                            NotificationType.Error);
                        return View(user);
                    }
                }
                catch (Exception e)
                {
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    this.AddNotification(e.Message, NotificationType.Error);
                    return View();
                }
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var user = _unitOfWork.Users.Get(id.Value);
                if (user == null)
                {
                    return NotFound();
                }
                var viewModel = _mapper.Map<CreateUserViewModel>(user);
                return View(viewModel);
            }
            catch (Exception e)
            {
                this.AddNotification(e.Message, NotificationType.Error);
                _historyService.LogError(e, HistoryErrorType.Middle);
                return View();
            }

        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateUserViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var orgUser = _mapper.Map<User>(user);
                    using (var res = new UserUpdate(_provider, _msgSender, _historyService))
                    {
                        var msg = (UpdateUserMessage)res.Update(orgUser);
                        if (msg == UpdateUserMessage.Success)
                        {
                            this.AddNotification(EnumExtention.GetDescription(msg), NotificationType.Success);
                            if (user.Image != null)
                            {

                                Gallery gal;
                                var dbgal = _unitOfWork.Galleries.GetGallery(orgUser.Id, (byte)GalleryKind.UserProfile);
                                int count = 0;
                                if (dbgal != null && dbgal.Any())
                                {
                                    gal = dbgal.FirstOrDefault();
                                    if (gal != null)
                                    {
                                        gal.POrder = 1;
                                        gal.Ext = Path.GetExtension(user.Image.FileName);
                                        _unitOfWork.Galleries.Update(gal);
                                    }
                                    count = _unitOfWork.Complete();
                                }
                                else
                                {
                                    gal = new Gallery()
                                    {
                                        Kind = (byte)GalleryKind.UserProfile,
                                        PublicId = orgUser.Id,
                                        State = (byte)GeneralState.Disable,
                                        Title = orgUser.FullName,
                                        Ext = Path.GetExtension(user.Image.FileName),
                                        POrder = 1
                                    };
                                    _unitOfWork.Galleries.Add(gal);
                                    count = _unitOfWork.Complete();
                                }
                                if (count > 0)
                                {
                                    var filePath = new Uploder(_hostingEnvironment, _historyService).UploadGallery(EncryptDecrypt.GetUrlHash(gal.Id.ToString() + gal.PublicId + gal.Kind), user.Image);

                                    if (string.IsNullOrEmpty(filePath))
                                        this.AddNotification("خطا در آپلود تصویر پروفایل.", NotificationType.Error);
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

        // GET: Admin/Users/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var user = _mapper.Map<CreateUserViewModel>(_unitOfWork.Users.Get(id.Value));

                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception e)
            {
                this.AddNotification(e.Message, NotificationType.Error);
                _historyService.LogError(e, HistoryErrorType.Middle);
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_unitOfWork.Users.CanDelete(id))
                {
                    _unitOfWork.Users.Remove(id);
                    _unitOfWork.Complete();
                    this.AddNotification("کاربر با موفقیت حذف شد.", NotificationType.Success);
                    return RedirectToAction("Index");
                }
                this.AddNotification("امکان حذف کاربر این کاربر وجود ندارد. ", NotificationType.Error);
                return RedirectToAction("Delete", new { id = id });
            }
            catch (Exception e)
            {
                this.AddNotification(e.Message, NotificationType.Error);
                _historyService.LogError(e, HistoryErrorType.Middle);
                return View();
            }
        }

        private bool ChefExists(int id)
        {
            return _unitOfWork.Users.IsExist(id);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dbUser = _unitOfWork.Users.Get(model.UserId);
                    if (dbUser != null)
                    {
                        var req = new UserChangePassword(_provider, _msgSender, _historyService).CahngePassword(new ChangePasswordDto() { UserName = dbUser.UserName, NewPassword = model.NewPass, ConfirmNewPassword = model.ConfirmNewPass, Ip = WebHelper.GetRemoteIP, IsAdmin = true });
                        //if (req == (byte)ChangePasswordUserMessage.Success)
                        //{

                        this.AddNotification(EnumExtention.GetDescription((ChangePasswordUserMessage)req), NotificationType.Success);
                        //var returnUrl = Url.Action("Edit", "Users", new { area = "Admin", id = model.UserId });
                        //await _cuser.LogOutAsync();
                        //return RedirectToAction("Login", "Account", new { area = "", returnUrl = returnUrl /* , LoginViewModel = new LoginViewModel() { Email = _cUser.GetEmail() }*/});
                        //}
                        //this.AddNotification(_localizer[EnumExtention.GetDescription((ChangePasswordUserMessage)req)].Value.ToString(), NotificationType.Error);
                        return RedirectToAction(nameof(Edit), new
                        {
                            id = model.UserId
                        });
                    }

                    this.AddNotification("کاربر مورد نظر یافت نشد.", NotificationType.Error);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception e)
                {
                    this.AddNotification(e.Message, NotificationType.Error);
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            this.AddNotification("خطا در تغییر رمز عبور.",NotificationType.Error);
            return RedirectToAction(nameof(Edit), new
            {
                id = model.UserId
            });
        }
    }
}
