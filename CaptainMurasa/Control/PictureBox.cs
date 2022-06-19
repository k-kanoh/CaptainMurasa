using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainMurasa
{
    public class PictureBox : System.Windows.Forms.PictureBox
    {
        public void SetImage(Image img)
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }

            Image = img;
        }
    }
}
