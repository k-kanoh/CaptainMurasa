using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CaptainMurasa
{
    public static class FileUtil
    {
        /// <summary>
        /// 空ファイルを作成します。
        /// </summary>
        public static void CreateFile(FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
                file.Directory.Refresh();
                file.Directory.Attributes |= FileAttributes.Hidden;
            }

            file.OpenWrite().Dispose();
        }

        /// <summary>
        /// 画像を保存しファイル名を返します。
        /// </summary>
        public static string SaveImage(DirectoryInfo di, Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                var data = memoryStream.ToArray();
                var hash = Util.GetSha1Hash(data);
                var save = di.Combine($"{hash}.png");

                if (!save.Exists)
                {
                    CreateFile(save);
                    File.WriteAllBytes(save.FullName, data);
                }

                return hash;
            }
        }
    }
}
