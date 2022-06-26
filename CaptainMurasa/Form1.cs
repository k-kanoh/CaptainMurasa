using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public partial class Form1 : BaseForm
    {
        private RecipeInfo RecipeInfo => (RecipeInfo)DataContext;
        private EvidenceInfo EvidenceInfo { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EvidenceInfo = RecipeInfo.CreateEvidenceInfo(1);
        }

        public override void Reload()
        {
            CaptureList.DataSource = EvidenceInfo.GetComboBoxItems();
            CaptureList.SelectedIndex = CaptureList.Items.Count - 1;
        }

        public override void PressControlC()
        {
            var image = MainImage.Image;

            if (image == null) return;

            Clipboard.SetImage(image);
        }

        public override void PressControlV()
        {
            var image = Clipboard.GetImage();

            if (image == null) return;

            MainImage.SetImage(image);

            var hash = FileUtil.SaveImage(RecipeInfo.WorkDir, image);

            EvidenceInfo.Add(hash);
        }

        public override void PressDelete()
        {

        }

        private void CaptureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fi = (FileInfo)CaptureList.SelectedObject;

            if (fi == null) return;

            var image = Image.FromFile(fi.FullName);

            MainImage.SetImage(image);
        }
    }
}
