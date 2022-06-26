using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public static class Extention
    {
        public static T GetBoundItem<T>(this DataGridViewRow row)
        {
            return (T)row.DataBoundItem;
        }

        public static byte[] GetBytes(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] GetBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string GetCombinePath(this DirectoryInfo di, string name)
        {
            return Path.Combine(di.FullName, name);
        }

        public static FileInfo Combine(this DirectoryInfo di, string name)
        {
            return new FileInfo(GetCombinePath(di, name));
        }

        public static IEnumerable<string> ReadAllLines(this FileInfo file)
        {
            FileUtil.CreateFile(file);

            foreach (var line in File.ReadAllLines(file.FullName))
                yield return line;
        }
    }
}
