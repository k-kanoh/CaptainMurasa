using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public class BaseForm : Form
    {
        /// <summary>
        /// データコンテキスト
        /// </summary>
        public object DataContext { get; private set; }

        /// <summary>
        /// 子画面
        /// </summary>
        protected List<BaseForm> ChildForms { get; set; } = new List<BaseForm>();

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
            var key = Util.KeyParse(keyData, out bool ctrl, out _, out _);

            if (ctrl)
            {
                switch (key)
                {
                    case Keys.C:
                        PressControlC();
                        break;

                    case Keys.V:
                        PressControlV();
                        break;

                    case Keys.R:
                        Reload();
                        break;

                    case Keys.W:
                        Close();
                        break;
                }

                return true;
            }

            if (key == Keys.F5)
            {
                Reload();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        public virtual void Reload()
        { }

        public virtual void PressControlC()
        { }

        public virtual void PressControlV()
        { }

        public virtual void PressDelete()
        { }

        /// <summary>
        /// 子画面をモーダレスで出します。親画面を閉じたとき子画面は自動で閉じます。
        /// </summary>
        public void ShowModeless<T>(object data = null) where T : BaseForm, new()
        {
            var newForm = new T();

            if (data != null)
                newForm.DataContext = data;

            newForm.Show();

            ChildForms.Add(newForm);
        }

        /// <summary>
        /// 子画面を出します。
        /// </summary>
        public void ShowDialog<T>(object data = null) where T : BaseForm, new()
        {
            using (var newForm = new T())
            {
                if (data != null)
                    newForm.DataContext = data;

                newForm.ShowDialog();
            }
        }
    }
}
