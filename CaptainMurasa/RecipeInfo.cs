using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace CaptainMurasa
{
    public class RecipeInfo : ICheckableGrid
    {
        /// <summary>
        /// レシピファイル
        /// </summary>
        public FileInfo RecipeFile { get; private set; }

        /// <summary>
        /// レシピファイルのディレクトリ
        /// </summary>
        public DirectoryInfo RecipeDir => RecipeFile.Directory;

        /// <summary>
        /// 作業用ディレクトリ
        /// </summary>
        public DirectoryInfo WorkDir
        {
            get
            {
                if (__workDir == null)
                    __workDir = new DirectoryInfo(RecipeDir.Parent.GetCombinePath(".Murasa"));

                return __workDir;
            }
        }

        /// <summary>
        /// 項番リスト
        /// </summary>
        public List<int> Numbers { get; private set; } = new List<int>();

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
        /// レシピキー
        /// </summary>
        public string RecipeKey { get; private set; }

        /// <summary>
        /// YAML
        /// </summary>
        public YamlNode Yaml { get; private set; }

        /// <summary>
        /// YAMLをパースできたか否か
        /// </summary>
        public bool IsBroken => !RecipeKey.Val();

        /// <summary>
        /// ☑
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecipeInfo(FileInfo file)
        {
            RecipeFile = file;

            if (IsRecipe)
            {
                ParseNo();
                LoadYaml();
            }
        }

        /// <summary>
        /// 項番を分解します
        /// </summary>
        private void ParseNo()
        {
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

        /// <summary>
        /// YAMLを読み込みます
        /// </summary>
        private void LoadYaml()
        {
            using (var sr = RecipeFile.OpenText())
            {
                try
                {
                    var yaml = new YamlStream();
                    yaml.Load(sr);

                    if (yaml.Documents.Any())
                    {
                        Yaml = yaml.Documents[0].RootNode;
                        RecipeKey = GetSha1Hash();
                    }
                }
                catch
                {
                    // 処理なし
                }
            }
        }

        /// <summary>
        /// 項番、レシピファイル、任意の番号を含めたキーを返します
        /// </summary>
        public string GetSha1Hash(int? i = default)
        {
            var bytes = Number.GetBytes();
            bytes = bytes.Concat(Yaml.ToString().GetBytes()).ToArray();

            if (i.HasValue)
                bytes = bytes.Concat(i.Value.GetBytes()).ToArray();

            return Util.GetSha1Hash(bytes);
        }

        /// <summary>
        /// エビデンスクラスを返します
        /// </summary>
        public EvidenceInfo CreateEvidenceInfo(int i)
        {
            return new EvidenceInfo(i, this);
        }

        private DirectoryInfo __workDir;
    }
}
