﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineCourse.Core
{
    public enum RegisterUserMessage
    {
        [Description("ثبت نام با موفقیت انجام شد.")]
        Success = 0,
        [Description("مویایل یا ایمیل نباید خالی باشد.")]
        MobileAndEmailIsNull = 1,
        [Description("ایمیل مورد نظر مجاز نمی باشد.")]
        EmailIsExist = 2,
        [Description("شماره همراه یافت نشد.")]
        MobileIsExist = 3,
        [Description("ایمیل صحیح نیست.")]
        EmailPatternNotValid = 4,
        [Description("شماره همراه صحیح نیست.")]
        MobilePatternNotValid = 5,
        [Description("ایمیل یافت نشد.")]
        PasswordIsNotValid = 6,
        [Description("ip شما مجاز نمی باشد.")]
        IpIsNotValid = 7,
        //[Description("ایمیل یافت نشد")]
        //Exception = 8,
    }
    public enum UpdateUserMessage
    {
        [Description("حساب کاربری با موفقیت ویرایش شد.")]
        Success = 0,
        [Description("مویایل یا ایمیل نباید خالی باشد.")]
        MobileAndEmailIsNull = 1,
        [Description("ایمیل یافت نشد.")]
        EmailIsNotExist = 2,
        [Description("شماره همراه یافت نشد.")]
        MobileIsNotExist = 3,
        [Description("ایمیل صحیح نیست.")]
        EmailPatternNotValid = 4,
        [Description("شماره همراه صحیح نیست.")]
        MobilePatternNotValid = 5,
        [Description("ip شما مجاز نمی باشد.")]
        IpIsNotValid = 7,
        [Description("حساب کاربری شما باموفقیت ویرایش شد,لطفا لوارد شوید.")]
        SuccessWithLogin = 8,
        //[Description("حساب کاربری با موفقیت ویرایش شد.")]
        //Exception = 8,
    }

    public enum ChangePasswordUserMessage
    {
        [Description("رمز عبور با موفقیت ویرایش شد.")]
        Success = 0,
        [Description("شماره همراه یا ایمیل نباید خالی باشد.")]
        MobileAndEmailIsNull = 1,
        [Description("ایمیل یافت نشد.")]
        EmailIsNotExist = 2,
        [Description("شماره همراه یافت نشد.")]
        MobileIsNotExist = 3,
        [Description("ایمیل صحیح نمی باشد.")]
        EmailPatternNotValid = 4,
        [Description("شماره همراه صحیح نمی باشد.")]
        MobilePatternNotValid = 5,
        [Description("رمز عبور صحیح نمی باشد.")]
        PasswordIsNotValid = 6,
        [Description("ip شما مجاز نمی باشد.")]
        IpIsNotValid = 7,
        [Description("رمز عبور جدید صحیح نمی باشد.")]
        NewPasswordNotValid = 9,
        [Description("تکرار رمز عبور جدید صحیح نمی باشد.")]
        ConfirmNewPasswordNotValid = 10,
    }
    public enum RecoveryUserMessage
    {
        [Description("رمز عبور جدید ارسال شد.")]
        Success = 0,
        [Description("ایمیل یافت نشد.")]
        EmailIsNotExist = 1,
        [Description("شماره همراه موجود نیست.")]
        MobileIsNotExist = 2,
        [Description("رمز عبور به تازگی ذخیره شده است, لطفا اندکی صبر کنید.")]
        LastResetIsClose = 3,
        [Description("خطای داپلیکیت")]
        DuplicateUser = 7,
        [Description("خطا")]
        Exception = 8,
    }

    public enum LoginUserMessage
    {
        [Description("ورود موفقیت آمیز.")]
        Success = 0,
        [Description("ایمیل یافت نشد.")]
        EmailIsNotExist = 1,
        [Description("شماره همراه موجود نیست.")]
        MobileIsNotExist = 2,
        [Description("رمز عبور اشتباه است.")]
        PasswordInCorrect = 3,
        [Description("حساب کاربری غیر فعال است.")]
        AccountDisabled = 4,
        [Description("حساب کاربری اعتبار سنجی نشده است.")]
        AccountNotVerified = 5,
        [Description("ip شما مجاز نمی باشد.")]
        IpNotValid = 7,
        [Description("خطا")]
        Exception = 8,


    }

    public enum VerifyUserMessage
    {
        [Description("حساب شما با موفقیت فعال شد.")]
        Success = 0,
        [Description("ایمیل یافت نشد.")]
        EmailIsNotExist = 1,
        [Description("شماره همراه یافت نشد.")]
        MobileIsNotExist = 2,
        [Description("کدفعال سازی معتبر نمیباشد.")]
        ActivationCodeInCorrect = 3,
        [Description("حساب شما غیرفعال است.")]
        AccountDisabled = 4,
        [Description("کد فعال سازی منقضی شده است.")]
        ActivationCodeExpired = 5,
        [Description("کد فعال سازی به تازگی ارسال شده لطفا اندکی صبر کنید.")]
        ActivationCodeSend = 6,
        [Description("ip شما محدود شده است.")]
        IpNotValid = 7,
        [Description("کد فعال سازی ارسال شد.")]
        ReqVerCodeSend =8,
        [Description("Exception")]
        Exception = 10,
    }

    public static class RegexConstatnts
    {
        public static string MobileRegex = @"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$";

        public static string EmailRegex { get; internal set; } = @"^ (? ("")("".+? (?< !\\)""@)|(([0 - 9a - z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
           + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
    }
}
