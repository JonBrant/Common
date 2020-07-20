using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


namespace Com.PI.Utilities {
    public static class StringExtensions {
        public static string ToTitleCase(this string inputString) { return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower()); }

        public static string FromTitleCase(this string inputString) { return Regex.Replace(inputString, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 "); }
    }
}