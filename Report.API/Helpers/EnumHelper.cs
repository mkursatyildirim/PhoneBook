using System.ComponentModel;
using System.Reflection;

namespace Report.API.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumVar) where T : IConvertible
        {
            FieldInfo fi = enumVar.GetType().GetField(enumVar.ToString());

            if (fi == null)
                return string.Empty;

            var description = fi.GetCustomAttribute<DescriptionAttribute>(false)?.Description ?? string.Empty;

            return description;
        }
    }
}
