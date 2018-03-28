using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OnlineCourse.Entity
{
    public static class EnumExtentionEntity
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
    }
}
