using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaptainMurasa
{
    public class RecipeInfo : ICheckableGrid
    {
        public RecipeInfo(FileInfo file)
        {
            RecipeFile = file;
            ParseNo();
        }

        /// <summary>
        /// レシピファイル
        /// </summary>
        public FileInfo RecipeFile { get; set; }

        /// <summary>
        /// 項番リスト
        /// </summary>
        public List<int> Numbers { get; set; } = new List<int>();

        /// <summary>
        /// 項番リストの先頭
        /// </summary>
        public int Number => Numbers.FirstOrDefault();

        /// <summary>
        /// 項番
        /// </summary>
        public string RecipeNo => RecipeTitle.Match(@"^([0-9][0-9-,\s]*)").Trim();

        /// <summary>
        /// レシピ名
        /// </summary>
        public string RecipeName => RecipeTitle.Match(@"\[(.*)\]$").Trim();

        /// <summary>
        /// ファイル名 (拡張子なし)
        /// </summary>
        public string FileNameWithoutExtention => Path.GetFileNameWithoutExtension(RecipeFile.Name).Trim();

        /// <summary>
        /// レシピタイトル (全体)
        /// </summary>
        public string RecipeTitle
        {
            get
            {
                if (FileNameWithoutExtention == "a")
                {
                    // ファイル名が「a」の場合は親ディレクトリ名を返します
                    return RecipeFile.Directory.Name.Trim();
                }
                else
                {
                    return FileNameWithoutExtention;
                }
            }
        }

        /// <summary>
        /// レシピファイルか否か
        /// </summary>
        public bool IsRecipe => RecipeTitle.IsMatch(@"^[0-9][0-9-,\s]*");

        /// <summary>
        /// ☑
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 項番を分解します
        /// </summary>
        private void ParseNo()
        {
            if (!IsRecipe) return;

            foreach (var splitByComma in RecipeNo.Split(','))
            {
                if (splitByComma.Trim().Match(@"([0-9]+)-([0-9]+)", out string begin, out string end))
                {
                    var i = int.Parse(begin);
                    var en = int.Parse(end);
                    do Numbers.Add(i++); while (i <= en);
                }
                else if (int.TryParse(splitByComma.Trim(), out int i))
                {
                    Numbers.Add(i);
                }
            }
        }
    }
}
