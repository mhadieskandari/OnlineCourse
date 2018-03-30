using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineCourse.Core
{
    public enum RegisterUserMessage
    {
        [Description("RegistrationSuccess")]
        Success = 0,
        [Description("MobileAndEmailIsNull")]
        MobileAndEmailIsNull = 1,
        [Description("EmailIsExist")]
        EmailIsExist = 2,
        [Description("MobileIsExist")]
        MobileIsExist = 3,
        [Description("EmailPatternNotValid")]
        EmailPatternNotValid = 4,
        [Description("MobilePatternNotValid")]
        MobilePatternNotValid = 5,
        [Description("ایمیل یافت نشد.")]
        PasswordIsNotValid = 6,
        [Description("IpIsNotValid")]
        IpIsNotValid = 7,
        //[Description("ایمیل یافت نشد")]
        //Exception = 8,
    }
    public enum UpdateUserMessage
    {
        [Description("UpdateAccuontSuccess")]
        Success = 0,
        [Description("MobileAndEmailIsNull")]
        MobileAndEmailIsNull = 1,
        [Description("EmailIsExist")]
        EmailIsNotExist = 2,
        [Description("MobileIsNotExist")]
        MobileIsNotExist = 3,
        [Description("EmailPatternNotValid")]
        EmailPatternNotValid = 4,
        [Description("MobilePatternNotValid")]
        MobilePatternNotValid = 5,
        [Description("PasswordIsNotValid")]
        PasswordIsNotValid = 6,
        [Description("IpIsNotValid")]
        IpIsNotValid = 7,
        [Description("UpdateAccuontSuccess")]
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
        [Description("NewPasswordSend")]
        Success = 0,
        [Description("EmailIsNotExist")]
        EmailIsNotExist = 1,
        [Description("MobileIsNotExist")]
        MobileIsNotExist = 2,
        [Description("ResetPasswordSendClosely")]
        LastResetIsClose = 3,
        [Description("DuplicateUser")]
        DuplicateUser = 7,
        [Description("Exception")]
        Exception = 8,
    }

    public enum LoginUserMessage
    {
        [Description("LoginSuccess")]
        Success = 0,
        [Description("EmailIsNotExist")]
        EmailIsNotExist = 1,
        [Description("MobileIsNotExist")]
        MobileIsNotExist = 2,
        [Description("PasswordInCorrect")]
        PasswordInCorrect = 3,
        [Description("AccountDisabled")]
        AccountDisabled = 4,
        [Description("AccountNotVerified.")]
        AccountNotVerified = 5,
        [Description("IpNotValid")]
        IpNotValid = 7,
        [Description("Exception")]
        Exception = 8,


    }

    public enum VerifyUserMessage
    {
        [Description("VerificationSuccess")]
        Success = 0,
        [Description("EmailIsNotExist")]
        EmailIsNotExist = 1,
        [Description("MobileIsNotExist")]
        MobileIsNotExist = 2,
        [Description("VerificationCodeIsNotValid")]
        ActivationCodeInCorrect = 3,
        [Description("AccountDisabled")]
        AccountDisabled = 4,
        [Description("VerificationCodeExpired")]
        ActivationCodeExpired = 5,
        [Description("VerificationCodeSentNearly")]
        ActivationCodeSend = 6,
        [Description("IpNotValid")]
        IpNotValid = 7,
        [Description("Exception")]
        Exception = 8,
    }

    public static class RegexConstatnts
    {
        public static string MobileRegex = @"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$";

        public static string EmailRegex { get; internal set; } = @"^ (? ("")("".+? (?< !\\)""@)|(([0 - 9a - z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
           + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
    }
}
