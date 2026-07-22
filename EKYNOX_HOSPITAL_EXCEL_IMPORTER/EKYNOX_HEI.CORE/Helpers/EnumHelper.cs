using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.CORE.Helpers
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class DataIdNameViewModel
    {
        [Browsable(false)]
        public int Id { get; set; }

        [DisplayName("Adı")]
        public string Name { get; set; }
    }

    public static class EnumHelper
    {
        public static List<DataIdNameViewModel> GetDisplayValues(Type enumType)
        {
            var enumValues = new List<DataIdNameViewModel>();
            foreach (System.Enum value in System.Enum.GetValues(enumType))
            {
                if (value.GetAttribute<DisplayAttribute>() != null)
                    enumValues.Add(new DataIdNameViewModel { Id = value.GetHashCode(), Name = value.GetAttribute<DisplayAttribute>().Name });
                else
                    enumValues.Add(new DataIdNameViewModel { Id = value.GetHashCode(), Name = value.GetAttribute<DisplayNameAttribute>().DisplayName });
            }

            return enumValues;
        }

        public static TAttribute GetAttribute<TAttribute>(this System.Enum enumValue)
        where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName(this System.Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));

            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }
    }
}
