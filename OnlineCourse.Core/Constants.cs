using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineCourse.Core
{

    public enum HistoryErrorType
    {
        Core = 0,
        Middle = 1,
        UI = 2
    }
    public enum HistoryState
    {
        New = 0,
        Saw = 1,
        Ok = 2
    }

    public enum UserAccessLevel
    {
        [Description("Stusent")]
        Stusent = 0,
        [Description("Teacher")]
        Teacher = 1,
        [Description("User")]
        User = 2,
        [Description("Accountant")]
        Accountant = 3,
        [Description("Administrator")]
        Administrator = 10
    }

    public enum UserState
    {
        [Description("DisApproved")]
        DisApproved = 0,
        [Description("Verified")]
        Verified = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Pending")]
        Pending = 10,
        [Description("Removed")]
        Removed = 20,
    }

    public enum GeneralState
    {
        //[DisplayName("Disable")]
        [Description("Disable")]
        Disable = 0,
        //[DisplayName("Enable")]
        [Description("Enable")]
        Enable = 1

    }

    public enum ValidationState
    {
        [Description("Invalid")]
        Invalid = 0,
        [Description("Valid")]
        Valid = 1

    }

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
        [Description("ChangePasswordSuccess")]
        Success = 0,
        [Description("MobileAndEmailIsNull")]
        MobileAndEmailIsNull = 1,
        [Description("EmailIsNotExist")]
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
        [Description("ChangePasswordSuccess")]
        SuccessWithLogin = 8,
        [Description("NewPasswordNotValid")]
        NewPasswordNotValid = 9,
        [Description("ConfirmNewPasswordNotValid")]
        ConfirmNewPasswordNotValid = 10,
        //[Description("حساب کاربری با موفقیت ویرایش شد.")]
        //Exception = 8,
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
