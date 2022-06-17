using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public class DataGridView: System.Windows.Forms.DataGridView
    {
        public DataGridView()
        {
            TabStop = false;
            RowHeadersVisible = false;
            ShowCellToolTips = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RowTemplate.DefaultCellStyle.Font = new Font("ＭＳ ゴシック", 9.75F);

            DataMember = null;
            DoubleBuffered = true;
            AutoGenerateColumns = false;
        }
    }
}
