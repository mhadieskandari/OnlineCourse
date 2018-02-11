using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineCourse.Entity
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

    public enum EducationLevel
    {
        ElOne = 0,
        ElTwo = 1,
        ElThree = 2,
        ElFour = 3,
        ElFive = 4,
        ElSix = 5,
        ImOne = 6,
        ImTwo = 7,
        ImThree = 8,
        HsOne = 9,
        HsTwo = 10,
        HsThree = 11,
        Bachelor = 12,
        Master = 13,
        Phd = 14
    }

    public enum PayType
    {
        Online = 0,
        Cash = 1,
        BankReceipt = 2
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
        Saturday = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        WednesDay = 4,
        Thursday = 5,
        Friday = 6
    }

    public enum GalleryKind
    {
        [Description("UserProfile")]
        UserProfile = 0,
        [Description("UserGallery")]
        UserGallery = 1,
    }

    public enum ActiveState
    {
        DeActive = 0,
        Active = 1
    }

    /// <summary>
    /// NotStarted, OnGoing, Complete, Crashed: In this Situation 'EndedTime = CrashedTime'
    /// </summary>
    public enum ClassStatus
    {
        NotStarted = 0,
        OnGoing = 1,
        Complete = 2,
        Crashed = 3
    }

    /// <summary>
    /// Teacher 'Received' OR 'Sent' the Doc
    /// </summary>
    public enum ClassDocKind
    {
        Received = 0,
        Sent = 1
    }
}
