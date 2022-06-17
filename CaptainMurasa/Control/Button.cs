using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainMurasa.Control
{
    public class Button : System.Windows.Forms.Button
    {
        [DefaultValue(typeof(Font), "メイリオ, 11.25pt")]
        public override Font Font { get => base.Font; set => base.Font = value; }

        public Button()
        {
            Font = new Font("メイリオ", 11.25F);
        }
    }
}
