using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public static class Util
    {
        private static readonly SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

        public static Keys KeyParse(Keys keyData, out bool ctrl, out bool alt, out bool shift)
        {
            alt = (keyData & Keys.Modifiers) == Keys.Alt;
            ctrl = (keyData & Keys.Modifiers) == Keys.Control;
            shift = (keyData & Keys.Modifiers) == Keys.Shift;
            return (keyData & Keys.KeyCode);
        }

        public static string GetSha1Hash(byte[] bytes)
        {
            return string.Join("", sha1.ComputeHash(bytes).Select(x => x.ToString("x2")));
        }
    }
}
