using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace OnlineCourse.Core.Extentions
{
    public class EnumExtention
    {
        public static string GetDescription(Enum enumType)
        {
            FieldInfo fi = enumType.GetType().GetField(enumType.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return enumType.ToString();
            }
        }

        public static string GetDisplayName(Enum enumType)
        {
            FieldInfo fi = enumType.GetType().GetField(enumType.ToString());
            DisplayNameAttribute[] attributes = (DisplayNameAttribute[])fi.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].DisplayName;
            }
            else
            {
                return enumType.ToString();
            }
        }
    }
}
