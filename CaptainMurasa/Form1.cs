using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Util.KeyParse(e.KeyData, out bool ctrl, out _, out _) == Keys.V && ctrl)
            {
                var image = Clipboard.GetImage();

                if (image == null)
                    return;

                pictureBox1.SetImage(image);

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    var buffer = memoryStream.ToArray();

                    File.WriteAllBytes(Path.Combine(@"C:\Users\kkano\Desktop\てすと", $"{Util.GetSha1Hash(buffer)}.png"), buffer);
                }
            }
        }
    }
}
