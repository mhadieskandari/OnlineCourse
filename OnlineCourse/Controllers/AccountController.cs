using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Panel.Utils.ViewModels.AccountViewModels;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows;
using OnlineCourse.Entity.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using OnlineCourse.Panel.Utils.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _provider;
        private readonly HistoryService _historyService;
        private readonly MessageService _msgSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CurrentUser _cUser;

        private readonly IHtmlLocalizer<SharedResource> _localizer;

        public AccountController(IUnitOfWork unitOfWork, IServiceProvider provider, HistoryService historyService, CurrentUser cUser, IHtmlLocalizer<SharedResource> localizer)
        {
            _unitOfWork = unitOfWork;
            _provider = provider;
            _historyService = historyService;
            _msgSender = new MessageService();
            _httpContextAccessor = new HttpContextAccessor();
            _cUser = cUser;
            _localizer = localizer;

        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Login(string returnUrl)
        {
            if (_cUser.IsAuthenticated())
            {
                var user =_cUser.GetUser().Result;
                var ret =LoginRedirect(user, returnUrl);
                if (ret != null)
                {
                    return ret;
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var loginWorkFlow = new UserLogin(_provider, _msgSender, _historyService)
                .Login(new LoginDto() { Email = loginModel.Email, PassWord = loginModel.Password, Ip = WebHelper.GetRemoteIP, Remember = loginModel.RememberMe });

            if (loginWorkFlow == (byte)LoginUserMessage.Success)
            {
                try
                {
                    var user = _cUser.Login(loginModel.Email, loginModel.Password, loginModel.RememberMe).Result;

                    if (user != null)
                    {
                        var ret = LoginRedirect(user, returnUrl);
                        if (ret != null)
                        {
                            return ret;
                        }
                    }

                    return Redirect(returnUrl);
                }
                catch (Exception e)
                {
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    this.AddNotification(e.Message, NotificationType.Error);
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            else if (loginWorkFlow == (byte)LoginUserMessage.AccountNotVerified)
            {
                var reqCode = new UserReqVerCode(_provider, _msgSender, _historyService).RequestCode(new ReqVerifyCodeDto() { Email = loginModel.Email, Ip = WebHelper.GetRemoteIP });
                if (reqCode == (byte)VerifyUserMessage.Success)
                {
                    this.AddNotification(EnumExtention.GetDescription((LoginUserMessage)loginWorkFlow), NotificationType.Success);
                }
                else
                {
                    this.AddNotification(EnumExtention.GetDescription((LoginUserMessage)loginWorkFlow), NotificationType.Error);
                }

                return RedirectToAction("SendCode", new { email = loginModel.Email });

                //return RedirectToAction("RequsetVerifyCode", new { email = loginModel.Email });
            }
            else
            {
                var msg = EnumExtention.GetDescription((LoginUserMessage)loginWorkFlow);
                this.AddNotification(_localizer[msg].Value.ToString(), NotificationType.Error);
            }
            return View();
        }

        private IActionResult LoginRedirect(User user,string returnUrl)
        {
            if (user != null)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    if (user.AccessLevel == UserAccessLevel.Administrator)
                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    if (user.AccessLevel == UserAccessLevel.Teacher)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Teacher" });
                    }

                    if (user.AccessLevel == UserAccessLevel.Stusent)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Student" });
                    }
                }
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home", new { area = "" });
            }
            return null;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.Authentication.SignOutAsync("CookieAuthentication");
            await _cUser.LogOutAsync();
            return RedirectToAction("Index", "Home");//_httpContextAccessor.HttpContext.Request.GetDisplayUrl()
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register(bool? IsTeacher)
        {
            var model = new RegisterViewModel();
            if (IsTeacher != null && IsTeacher == true)
            {
                model.IsTeacher = 1;
            }
            else
            {
                model.IsTeacher = 0;
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var level = (registerViewModel.IsTeacher != null && registerViewModel.IsTeacher.Value == 1) ? UserAccessLevel.Teacher : UserAccessLevel.Stusent;
            var user = new User()
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                FullName = registerViewModel.FullName,
                Mobile = registerViewModel.Mobile,
                Password = registerViewModel.Password,
                State = UserState.Pending,
                RegisterDate = DateTime.Now,
                LastLoginIp = WebHelper.GetRemoteIP,
                AccessLevel = level
            };

            var register = new UserRegistration(_provider, _msgSender, _historyService);
            var res = register.Register(user);
            if (res == (byte)RegisterUserMessage.Success)
            {
                var reqCode = new UserReqVerCode(_provider, _msgSender, _historyService).RequestCode(new ReqVerifyCodeDto() { Email = registerViewModel.Email, Ip = WebHelper.GetRemoteIP });
                if (reqCode == (byte)VerifyUserMessage.Success)
                {
                    var msg = EnumExtention.GetDescription((VerifyUserMessage)reqCode);
                    this.AddNotification(_localizer[msg].Value.ToString(), NotificationType.Success);
                    return RedirectToAction("SendCode", new { email = registerViewModel.Email });
                }
                this.AddNotification("Success", NotificationType.Success);
                return RedirectToAction("RequsetVerifyCode", new { email = registerViewModel.Email });
            }
            else
            {
                this.AddNotification(EnumExtention.GetDescription((RegisterUserMessage)res), NotificationType.Error);
                return View(registerViewModel);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User()
            {
                Mobile = forgotPasswordViewModel.Mobile
            };

            try
            {
                var recover = new UserResetPassword(_provider, _msgSender, _historyService);
                var res = (RecoveryUserMessage)recover.Recovery(user);
                var msg = EnumExtention.GetDescription(res);

                this.AddNotification(_localizer[msg].Value.ToString(), res == RecoveryUserMessage.Success ? NotificationType.Success : NotificationType.Error);
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                this.AddNotification(e.Message, NotificationType.Error);
                _historyService.LogError(e, HistoryErrorType.Middle);
                return View(forgotPasswordViewModel);
            }


        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RequsetVerifyCode(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return View(new SendCodeViewModel() { Email = email });
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequsetVerifyCode(SendCodeViewModel sendCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var req = new UserReqVerCode(_provider, _msgSender, _historyService).RequestCode(new ReqVerifyCodeDto() { Email = sendCode.Email, Ip = sendCode.Ip });

                    if (req == (byte)VerifyUserMessage.Success || req == (byte)VerifyUserMessage.ActivationCodeSend)
                    {
                        this.AddNotification(EnumExtention.GetDescription((VerifyUserMessage)req), NotificationType.Info);
                        return RedirectToAction("SendCode", sendCode);
                    }
                    else
                    {
                        this.AddNotification(EnumExtention.GetDescription((VerifyUserMessage)req), NotificationType.Info);
                        return RedirectToAction("SendCode", sendCode);
                    }
                }
                catch (Exception e)
                {
                    this.AddNotification(e.Message, NotificationType.Error);
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            return View(sendCode);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SendCode(SendCodeViewModel sendCode)
        {
            return View(sendCode);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("SendCode")]
        public IActionResult SendCodePost(SendCodeViewModel sendCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var req = new UserVerify(_provider, _msgSender, _historyService).Update(new VerifyDto() { Email = sendCode.Email, Ip = sendCode.Ip, VerificationCode = sendCode.Code });

                    if (req == (byte)VerifyUserMessage.Success)
                    {
                        var user = _unitOfWork.Users.GetByEmail(sendCode.Email).FirstOrDefault();
                        if (user != null && user.AccessLevel == UserAccessLevel.Stusent)
                        {
                            IActionResult action = Login(new LoginViewModel() { Email = user.Email, Password = EncryptDecrypt.Decrypt(user.Password), RememberMe = true }, "/Student/Profile");
                            return action;
                        }
                        else if (user != null && user.AccessLevel == UserAccessLevel.Teacher)
                        {
                            IActionResult action = Login(new LoginViewModel() { Email = user.Email, Password = EncryptDecrypt.Decrypt(user.Password), RememberMe = true }, "/Teacher/Profile");
                            return action;
                        }

                        this.AddNotification(EnumExtention.GetDescription((VerifyUserMessage)req), NotificationType.Success);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        this.AddNotification(EnumExtention.GetDescription((VerifyUserMessage)req), NotificationType.Error);
                        return View(sendCode);
                    }


                }
                catch (Exception e)
                {
                    this.AddNotification(e.Message, NotificationType.Error);
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            return View(sendCode);
        }


        [Authorize()]
        [HttpGet]
        public async Task<IActionResult> UpdateInfo()
        {

            var userId = await _cUser.GetUserId();
            if (userId != null)
            {
                var user = await _unitOfWork.Users.GetAsync(userId.Value);
                var model = new UserUpdateViewModel() { Id = user.Id, Addrress = user.Addrress, City = user.City, Email = user.Email, FullName = user.FullName, Phone = user.Phone, UserName = user.UserName};
                return View(model);
            }
            this.AddNotification("اطلاعات کاربری یافت نشد.", NotificationType.Error);
            return RedirectToAction("NotFoundPage", "Home");
        }

        [Authorize()]
        [HttpPost]
        public async Task<IActionResult> UpdateInfo(UserUpdateViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var req = new UserUpdate(_provider, _msgSender, _historyService).Update(new User() { Id = user.Id, Addrress = user.Addrress, City = user.City, Email = user.Email, FullName = user.FullName,Phone = user.Phone, UserName = user.UserName});

                    if (req == (byte)UpdateUserMessage.SuccessWithLogin)
                    {

                        this.AddNotification(EnumExtention.GetDescription((UpdateUserMessage)req), NotificationType.Success);
                        await _cUser.LogOutAsync();
                        return RedirectToAction("Login", new LoginViewModel() { Email = user.Email });
                    }
                    else if (req == (byte)UpdateUserMessage.Success)
                    {
                        this.AddNotification(EnumExtention.GetDescription((UpdateUserMessage)req), NotificationType.Success);
                    }
                    else
                    {
                        this.AddNotification(EnumExtention.GetDescription((UpdateUserMessage)req), NotificationType.Error);
                    }


                }
                catch (Exception e)
                {
                    this.AddNotification(e.Message, NotificationType.Error);
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            return View(user);
        }


        [Authorize()]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var userid = await _cUser.GetUserId();
            var model = new ChangePasswordViewModel() { UserId = userid.Value };
            return View(model);
        }

        [Authorize()]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var req = new UserChangePassword(_provider, _msgSender, _historyService).CahngePassword(new ChangePasswordDto() { UserName = _cUser.GetEmail(), Password = model.OldPass, NewPassword = model.NewPass, ConfirmNewPassword = model.ConfirmNewPass, Ip = WebHelper.GetRemoteIP });

                    if (req == (byte)ChangePasswordUserMessage.Success)
                    {

                        this.AddNotification(_localizer[EnumExtention.GetDescription((ChangePasswordUserMessage)req)].Value.ToString(), NotificationType.Success);
                        await _cUser.LogOutAsync();
                        return RedirectToAction("Login", new LoginViewModel() { Email = _cUser.GetEmail() });
                    }

                    this.AddNotification(_localizer[EnumExtention.GetDescription((ChangePasswordUserMessage)req)].Value.ToString(), NotificationType.Error);
                }
                catch (Exception e)
                {
                    this.AddNotification(e.Message, NotificationType.Error);
                    _historyService.LogError(e, HistoryErrorType.Middle);
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            return RedirectToAction("UpdateInfo");
        }


    }

}
