using System.ComponentModel;

namespace Application.Utils
{
    public static class EnumUtils
    {
        public static string Description(this System.Enum enumValue)
        {
            var descriptionAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttribute?.Description ?? "";
        }
    }
}
