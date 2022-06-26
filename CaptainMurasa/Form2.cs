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
            Reload();
        }

        public override void Reload()
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

            Grid.SetDataSource(recipeInfos.OrderBy(x => x.Number).ToList());
        }

        private void Grid_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (var row in Grid.CastRows())
            {
                var item = row.GetBoundItem<RecipeInfo>();

                if (item.IsBroken)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    row.DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = Grid.GetSelectedItem<RecipeInfo>();
            var child = ChildForms.SingleOrDefault(x => ((RecipeInfo)x.DataContext).RecipeKey == item.RecipeKey);

            if (child != null)
            {
                child.Activate();
            }
            else
            {
                ShowModeless<Form1>(item);
            }
        }
    }
}
