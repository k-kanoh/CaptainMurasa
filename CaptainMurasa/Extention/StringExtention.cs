using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaptainMurasa
{
    public static class StringExtention
    {
        /// <summary>
        /// 有効な文字列の場合 true を返します。
        /// </summary>
        public static bool Val(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 文字列が指定された正規表現にマッチするか判定します。
        /// </summary>
        public static bool IsMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// 文字列を正規表現でパターンマッチングします。
        /// </summary>
        /// <param name="index">(0起算)</param>
        public static string Match(this string value, string pattern, int index = 0)
        {
            return Regex.Match(value, pattern).Groups.Cast<Group>().Select(x => x.Value).Skip(index + 1).FirstOrDefault() ?? "";
        }

        /// <summary>
        /// 文字列を正規表現でパターンマッチングします。
        /// </summary>
        public static bool Match(this string value, string pattern, out string grp1, out string grp2)
        {
            var m = Regex.Match(value, pattern);
            var groups = m.Groups.Cast<Group>().Select(x => x.Value).Skip(1).ToList();
            grp1 = (groups.Count() > 0) ? groups[0] : "";
            grp2 = (groups.Count() > 1) ? groups[1] : "";

            return m.Success;
        }

        /// <summary>
        /// 文字列を正規表現で分割します。
        /// </summary>
        public static string[] Split(this string value, string pattern)
        {
            return Regex.Split(value, pattern);
        }
    }
}
