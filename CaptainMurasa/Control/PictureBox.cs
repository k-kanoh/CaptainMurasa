using System.Drawing;

namespace CaptainMurasa
{
    public class PictureBox : System.Windows.Forms.PictureBox
    {
        public void SetImage(Image image)
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }

            Image = image;
        }
    }
}
