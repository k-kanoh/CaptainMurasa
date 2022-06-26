using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaptainMurasa
{
    public class EvidenceInfo
    {
        /// <summary>
        /// レシピクラス
        /// </summary>
        public RecipeInfo Parent { get; private set; }

        /// <summary>
        /// エビデンスキー
        /// </summary>
        public string EvidenceKey { get; private set; }

        /// <summary>
        /// エビデンスファイル
        /// </summary>
        public FileInfo EvidenceFile { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EvidenceInfo(int no, RecipeInfo recipe)
        {
            Parent = recipe;
            EvidenceKey = Parent.GetSha1Hash(no);
            EvidenceFile = Parent.WorkDir.Combine(EvidenceKey);
        }

        /// <summary>
        /// エビデンスをComboBoxItemに展開し返します。
        /// </summary>
        public IList<ComboBoxItem> GetComboBoxItems()
        {
            var empty = new List<ComboBoxItem>() { new ComboBoxItem() };
            var lines = EvidenceFile.ReadAllLines().Select((x, i) => new { i, fi = Parent.WorkDir.Combine($"{x}.png") });
            var items = from a in lines
                        where a.fi.Exists
                        select new ComboBoxItem()
                        {
                            Code = a.i + 1,
                            Name = a.fi.LastWriteTime.ToString("yyyy/MM/dd (ddd) HH:mm:ss"),
                            Object = a.fi
                        };

            return empty.Concat(items).ToList();
        }

        /// <summary>
        /// エビデンスを登録します。
        /// </summary>
        public void Add(string hash)
        {
            File.AppendAllLines(EvidenceFile.FullName, new[] { hash });
        }
    }
}
