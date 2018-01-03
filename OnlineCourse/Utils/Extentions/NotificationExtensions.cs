using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace OnlineCourse.Panel.Utils.Extentions
{
    public static class NotificationExtensions
    {
        private static readonly IDictionary<string, string> NotificationKey = new Dictionary<string, string>
        {
            { "Error",      "App.Notifications.Error" },
            { "Warning",    "App.Notifications.Warning" },
            { "Success",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };



        public static void AddErrorNotification(this Controller controller, string message)
        {
            AddNotification(controller, message, NotificationType.Error);
        }
        public static void AddErrorNotifications(this Controller controller, List<string> messages)
        {
            foreach (var message in messages)
            {
                AddErrorNotification(controller, message);
            }
        }
        public static void AddWarningNotification(this Controller controller, string messagee)
        {
            AddNotification(controller, messagee, NotificationType.Warning);
        }
        public static void AddSuccessNotification(this Controller controller, string message)
        {
            AddNotification(controller, message, NotificationType.Success);
        }
        public static void AddInfoNotification(this Controller controller, string message)
        {
            AddNotification(controller, message, NotificationType.Info);
        }


        public static void AddNotification(this Controller controller, string message, NotificationType notificationType)
        {
            var notificationKey = GetNotificationKeyByType(notificationType.ToString());
            var messages = controller.ViewData[notificationKey] as List<string> ?? new List<string>();
            messages.Add(message);
            controller.TempData[notificationKey] = messages;
            //controller.ViewData[notificationKey] = messages;
        }

        public static IEnumerable<string> GetNotifications(this IHtmlHelper htmlHelper, NotificationType notificationType)
        {
            string notificationKey = GetNotificationKeyByType(notificationType.ToString());
            return htmlHelper.ViewContext.TempData[notificationKey] as ICollection<string>;
            //return htmlHelper.ViewContext.ViewData[notificationKey] as ICollection<string>;
        }

        private static string GetNotificationKeyByType(string notificationType)
        {
            try
            {
                return NotificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
    }

    public enum NotificationType
    {
        Error,
        Warning,
        Success,
        Info
    }

}
