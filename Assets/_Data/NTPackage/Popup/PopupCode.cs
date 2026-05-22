using System.Collections.Generic;
using System.Reflection;

namespace NTPackage.UI
{
    public static class PopupCode
    {
        public const string Unknown = "Unknown";
        public const string LoadingUI = "LoadingUI";
        public const string SplashUI = "SplashUI";
        public const string MessagePanel = "MessagePanel";
    }

    public static class PopupCodeParser
    {
        private static readonly HashSet<string> ValidCodes = BuildValidCodes();

        public static string FromString(string name)
        {
            if (string.IsNullOrEmpty(name)) return PopupCode.Unknown;
            return ValidCodes.Contains(name) ? name : PopupCode.Unknown;
        }

        public static bool IsValid(string name)
        {
            return !string.IsNullOrEmpty(name) && ValidCodes.Contains(name);
        }

        private static HashSet<string> BuildValidCodes()
        {
            var codes = new HashSet<string>();
            var fields = typeof(PopupCode).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                if (!field.IsLiteral || field.IsInitOnly) continue;
                if (field.FieldType != typeof(string)) continue;
                if (field.Name == nameof(PopupCode.Unknown)) continue;

                var value = field.GetValue(null) as string;
                if (!string.IsNullOrEmpty(value)) codes.Add(value);
            }
            return codes;
        }
    }
}
