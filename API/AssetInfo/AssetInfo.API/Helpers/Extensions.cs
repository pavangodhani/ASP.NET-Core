using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API
{
    public static class Extensions
    {
        public static string ToLowerAndRemoveSpaces(this string str)
        {
            return str.ToLower().Replace(" ", String.Empty);
        }

        public static string ToLowerAndRemoveSpacesAnds(this string str)
        {
            return str.ToLower().Replace(" ", String.Empty).Replace("s", String.Empty);
        }

        public static string ToProper(this string str)
        {
            string newStr = null;

            var parts = str.Split(" ");

            var charArr = parts[0].ToCharArray();
            charArr[0] = Char.ToUpper(charArr[0]);
            newStr = new String(charArr);

            parts[0] = newStr;

            return string.Join(" ", parts);
        }
    }
}
