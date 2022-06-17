using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public partial class Form2 : BaseForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var root = @"C:\Users\kkano\Desktop\てすと";

            var rootInfo = new DirectoryInfo(root);
            var yamls = rootInfo.GetFiles("*.yml", SearchOption.AllDirectories).Concat(rootInfo.GetFiles("*.yaml", SearchOption.AllDirectories));

            var recipeInfos = new List<RecipeInfo>();

            foreach (var yaml in yamls)
            {
                var recipe = new RecipeInfo(yaml);
                if (recipe.IsRecipe)
                    recipeInfos.Add(new RecipeInfo(yaml));
            }
        }
    }
}
