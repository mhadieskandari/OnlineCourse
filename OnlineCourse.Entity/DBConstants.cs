using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineCourse.Entity
{
    public enum HistoryErrorType
    {
        [Description("هسته")]
        Core = 0,
        [Description("میانی")]
        Middle = 1,
        [Description("واسط کاربری")]
        UI = 2
    }
    public enum HistoryState
    {
        [Description("جدید")]
        New = 0,
        [Description("دیده شده")]
        Saw = 1,
        [Description("در حال بررسی")]
        Pending = 2,
        [Description("حل شده")]
        Ok = 3
    }

    public enum UserAccessLevel
    {
        [Description("دانش آموز/دانشجو")]
        Stusent = 0,
        [Description("معلم/استاد")]
        Teacher = 1,
        [Description("کاربر")]
        User = 2,
        [Description("حسابدار")]
        Accountant = 3,
        [Description("مدیر سیستم")]
        Administrator = 10
    }

    public enum UserState
    {
        [Description("رد شده")]
        DisApproved = 0,
        [Description("اعتبار سنجی شده")]
        Verified = 1,
        [Description("تایید شده")]
        Approved = 2,
        [Description("در حال انتظار")]
        Pending = 10,
        [Description("حذف شده")]
        Removed = 20,
    }

    public enum GeneralState
    {
        //[DisplayName("Disable")]
        [Description("غیرفعال")]
        Disable = 0,
        //[DisplayName("Enable")]
        [Description("فعال")]
        Enable = 1

    }

    public enum BankId
    {
        [Description("ملت")]
        Mellat = 0,
        [Description("پارسیان")]
        Parsian = 1,
        [Description("ملی")]
        Melli = 1,

    }

    public enum TermState
    {
        [Description("غیرفعال")]
        Disable = 0,
        [Description("بزودی")]
        CommingSoon = 1,
        [Description("درحال برگزاری")]
        Enable = 2,        
        [Description("پایان یافته")]
        Finish =3

    }

    public enum ValidationState
    {
        [Description("نامعتبر")]
        Invalid = 0,
        [Description("معتبر")]
        Valid = 1

    }

    public enum EducationLevel
    {
        [Description("پایه اول")]
        ElOne = 0,
        [Description("پایه دوم")]
        ElTwo = 1,
        [Description("پایه سوم")]
        ElThree = 2,
        [Description("پایه چهارم")]
        ElFour = 3,
        [Description("پایه پنجم")]
        ElFive = 4,
        [Description("پایه ششم")]
        ElSix = 5,
        [Description("پایه هفتم")]
        ImOne = 6,
        [Description("پایه هشتم")]
        ImTwo = 7,
        [Description("پایه نهم")]
        ImThree = 8,
        [Description("پایه دهم")]
        HsOne = 9,
        [Description("پایه یازدهم")]
        HsTwo = 10,
        [Description("پایه دوازدهم")]
        HsThree = 11,
        [Description("کاردانی")]
        PreBachelor = 12,
        [Description("کارشناسی")]
        Bachelor = 13,
        [Description("کارشناسی ارشد")]
        Master = 14,
        [Description("دکتری")]
        Phd = 15
    }

    public enum PayType
    {
        [Description("اینترنتی")]
        Online = 0,
        [Description("نقدی")]
        Cash = 1,
        [Description("فیش بانکی")]
        BankReceipt = 2
    }

    public enum PayState
    {
        [Description("درحال انتظار")]
        Pending = 0,
        [Description("موفق")]
        Approved = 1,
        [Description("ناموفق")]
        DisApproved = 2,
        [Description("بازگردانده شده")]
        PayBack = 2
    }

    public enum TermType
    {
        [Description("PluralInPlace")]
        PluralInPlace = 0,
        [Description("SingularInPlace")]
        SingularInPlace = 1,
        [Description("SingularOutPlace")]
        SingularOutPlace = 2
    }

    public enum WeekDays
    {
        [Description("شنبه")]
        Saturday = 0,
        [Description("یکشنبه")]
        Sunday = 1,
        [Description("دوشنبه")]
        Monday = 2,
        [Description("سه شنبه")]
        Tuesday = 3,
        [Description("چهارشنبه")]
        WednesDay = 4,
        [Description("پنجشنبه")]
        Thursday = 5,
        [Description("جمعه")]
        Friday = 6
    }

    public enum GalleryKind
    {
        [Description("تصویر پروفایل")]
        UserProfile = 0,
        [Description("گالری")]
        UserGallery = 1,
    }

    public enum ActiveState
    {
        [Description("غیرفعال")]
        DeActive = 0,
        [Description("فعال")]
        Active = 1
    }

    /// <summary>
    /// NotStarted, OnGoing, Complete, Crashed: In this Situation 'EndedTime = CrashedTime'
    /// </summary>
    public enum ClassStatus
    {
        [Description("شروع نشده")]
        NotStarted = 0,
        [Description("درحال برگزاری")]
        OnGoing = 1,
        [Description("به اتمام رسیده")]
        Complete = 2,
        [Description("منتفی شده")]
        Crashed = 3
    }

    /// <summary>
    /// Teacher 'Received' OR 'Sent' the Doc
    /// </summary>
    public enum ClassDocKind
    {
        [Description("دریافت شده")]
        Received = 0,
        [Description("ارسال شده")]
        Sent = 1
    }
}
