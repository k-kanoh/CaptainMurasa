using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public class DataGridView : System.Windows.Forms.DataGridView
    {
        private CheckBox allCheckBox;

        public DataGridView()
        {
            TabStop = false;
            RowHeadersVisible = false;
            ShowCellToolTips = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RowTemplate.DefaultCellStyle.Font = new Font("メイリオ", 9.75F);
            RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 192, 255);
            RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            RowTemplate.Height = 25;

            DataMember = null;
            DoubleBuffered = true;
            AutoGenerateColumns = false;

            allCheckBox = new System.Windows.Forms.CheckBox();
            allCheckBox.CheckedChanged += AllCheckBox_CheckedChanged;
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (Columns[0] is DataGridViewCheckBoxColumn && e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                using (Bitmap bmp = new Bitmap(30, 30))
                {
                    allCheckBox.BackColor = SystemColors.ControlLightLight;
                    allCheckBox.DrawToBitmap(bmp, new Rectangle(0, 0, 13, 18));

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, new Point(10, 3));
                    e.Handled = true;
                }
            }
        }

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            // まとめて☑をクリックしたとき
            if (Columns[0] is DataGridViewCheckBoxColumn && e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                allCheckBox.Checked = !allCheckBox.Checked;
            }

            base.OnCellClick(e);
        }

        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentCell != null && CurrentCell.ColumnIndex == 0)
            {
                CurrentCell = CurrentRow.Cells[1];
            }

            foreach (var row in CastRows())
            {
                if (row.DataBoundItem is ICheckableGrid item && item != null)
                {
                    item.IsChecked = allCheckBox.Checked;
                }
            }

            Refresh();
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            // 変な場所をダブルクリックした時は抜ける
            if (Columns[0] is DataGridViewCheckBoxColumn && e.ColumnIndex == 0 || e.RowIndex == -1)
                return;

            base.OnCellDoubleClick(e);
        }

        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            if (IsCurrentCellDirty)
            {
                CommitEdit(DataGridViewDataErrorContexts.Commit);
                base.OnCurrentCellDirtyStateChanged(e);
                Refresh();
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            // 変な場所をクリックした時は抜ける
            if (e.RowIndex == -1)
                return;

            base.OnCellContentClick(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Columns[0] is DataGridViewCheckBoxColumn && e.KeyCode == Keys.Space)
            {
                var i = 0;
                var val = true;
                foreach (var row in SelectedRows.Cast<DataGridViewRow>())
                {
                    if (row.DataBoundItem is ICheckableGrid item && item != null)
                    {
                        if (i++ == 0)
                            val = !item.IsChecked;

                        item.IsChecked = val;
                    }
                }

                Refresh();
            }

            base.OnKeyDown(e);
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            allCheckBox.Checked = false;
            Refresh();
        }

        /// <summary>
        /// 表示データのリストをグリッドのデータソースにバインドします。
        /// </summary>
        public void SetDataSource<T>(IList<T> dataSource)
        {
            DataSource = new BindingList<T>(dataSource);
        }

        /// <summary>
        /// グリッドのデータソースにバインドされているアイテムリストを取り出します。
        /// </summary>
        public IList<T> GetDataSource<T>()
        {
            var dataSource = (IBindingList)DataSource;
            return (dataSource != null) ? dataSource.OfType<T>().ToList() : new List<T>();
        }

        /// <summary>
        /// 選択中の行からバインドされたオブジェクトを取り出します。
        /// </summary>
        public T GetSelectedItem<T>()
        {
            return SelectedRows.Cast<DataGridViewRow>()
                .Select(x => x.DataBoundItem).OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Rows をジェネリックコレクションにキャストした状態で取り出します。
        /// </summary>
        public IEnumerable<DataGridViewRow> CastRows()
        {
            return Rows.Cast<DataGridViewRow>();
        }

        /// <summary>
        /// Columns をジェネリックコレクションにキャストした状態で取り出します。
        /// </summary>
        public IEnumerable<DataGridViewColumn> CastColumns()
        {
            return Columns.Cast<DataGridViewColumn>();
        }
    }
}
