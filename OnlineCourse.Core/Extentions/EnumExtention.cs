using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OnlineCourse.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OnlineCourse.Core.Extentions
{
    public static class EnumExtention
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


        public static SelectList GetEnumSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum enumObj)
           where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString(CultureInfo.InvariantCulture) };
            return new SelectList(values, "Id", "Name", enumObj);
        }
        public static SelectList GetEnumKeyKeySelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {
                Text = v.ToString(CultureInfo.InvariantCulture),
                Value = v.ToString(CultureInfo.InvariantCulture)
            });
            return new SelectList(values, "Text", "Value", enumObj);
        }


        public static SelectList GetEnumValueValueSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {
                Text = v.ToString("d", null),
                Value = v.ToString("d", null)
            });
            return new SelectList(values, "Text", "Value", enumObj);
        }

        public static SelectList GetEnumKeyValueValueSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {
                Text = v.ToString("d", null),
                Value = string.Format("{0} ({1})", v.ToString("d", null), v.ToString(CultureInfo.InvariantCulture)),
            });
            return new SelectList(values, "Text", "Value", enumObj);
        }

        public static SelectList GetEnumValueDescriptionSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {

                Text = v.ToString(CultureInfo.InvariantCulture),
                Value = (v as Enum).Description(),
            });
            return new SelectList(values, "Text", "Value", enumObj);
        }

        public static SelectList GetEnumKeyDescriptionSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {

                Text = v.ToString("d", null),
                Value = (v as Enum).Description(),
            });
            return new SelectList(values, "Text", "Value", enumObj);
        }






        //public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        //{
        //    var values = (from TEnum e in Enum.GetValues(typeof(TEnum))
        //        select new { ID = e, Name = (e as Enum).Description() }).ToList();

        //    return new SelectList(values, "Id", "Name", enumObj);
        //}











        public static string Description(this Enum value)
        {
            if (value == null)
                return string.Empty;
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return string.Empty;
            var attributes = field.GetCustomAttributes(false);
            if (attributes == null)
                return string.Empty;
            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            // return description
            return displayAttribute?.Description ?? "Description Not Found";
        }





        public static SelectList GetEnumKeyValueSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj, IStringLocalizer<SharedResource> Localizer)
           where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {

                Text = v.ToString("d", null),
                Value = Localizer[(v as Enum).ToString()].ToString(),
            });
            return enumObj.HasValue? new SelectList(values, "Text", "Value", enumObj.Value.GetHashCode()) : new SelectList(values, "Text", "Value");             
        }

        public static SelectList GetEnumKeyValueSelectList<TEnum>(this IHtmlHelper htmlHelper, TEnum? enumObj)
          where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new SelectListItem
            {

                Text = v.ToString("d", null),
                Value = (v as Enum).ToString()
            });
            var ss = enumObj.Value.GetHashCode();
            return new SelectList(values, "Text", "Value", ss);
        }
    }
}
