using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public class BaseForm: Form
    {
        [DefaultValue(typeof(Font), "メイリオ, 9.75pt")]
        public override Font Font { get => base.Font; set => base.Font = value; }

        public BaseForm()
        {
            Font = new Font("メイリオ", 9.75F);
        }

        /// <summary>
        /// ショートカットキーの実装
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Util.KeyParse(keyData, out bool ctrl, out _, out _) == Keys.W && ctrl)
            {
                Close();
                return true;
            }

            if (Util.KeyParse(keyData, out ctrl, out _, out _) == Keys.R && ctrl)
            {
                Reload();
                return true;
            }

            if (Util.KeyParse(keyData, out _, out _, out _) == Keys.F5)
            {
                Reload();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        public virtual void Reload() { }
    }
}
