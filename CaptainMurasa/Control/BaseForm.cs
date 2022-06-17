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
    }
}
