using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CaptainMurasa
{
    public class TextBox : System.Windows.Forms.TextBox
    {
        private int undoIndex = 0;
        private bool recording = true;
        private List<UndoItem> UndoBuffer;

        public TextBox()
        {
            Clear();
            ContextMenuStrip = new UndoableContextMenuStrip(this);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (recording)
            {
                while (undoIndex < UndoBuffer.Count - 1)
                    UndoBuffer.RemoveAt(UndoBuffer.Count - 1);

                UndoBuffer.Add(new UndoItem(SelectionStart, Text));
                undoIndex = UndoBuffer.Count - 1;
            }
        }

        public new void Undo()
        {
            if (!CanUndo) return;

            try
            {
                recording = false;
                var undo = UndoBuffer[--undoIndex];
                Text = undo.Text;
                SelectionStart = undo.Position;
            }
            finally
            {
                recording = true;
            }
        }

        public void Redo()
        {
            if (!CanRedo) return;

            try
            {
                recording = false;
                var undo = UndoBuffer[++undoIndex];
                Text = undo.Text;
                SelectionStart = undo.Position;
            }
            finally
            {
                recording = true;
            }
        }

        public void SetDefaultText(string text)
        {
            try
            {
                recording = false;
                UndoBuffer = new List<UndoItem>() { new UndoItem(0, text) };
                undoIndex = 0;
            }
            finally
            {
                recording = true;
            }
        }

        public new void Clear()
        {
            SetDefaultText("");
        }

        public new bool CanUndo => (undoIndex > 0);

        public bool CanRedo => (undoIndex < UndoBuffer.Count - 1);

        #region UndoItemクラス

        private class UndoItem
        {
            public int Position { get; set; }

            public string Text { get; set; }

            public UndoItem(int pos, string text)
            {
                Position = pos;
                Text = text;
            }
        }

        #endregion UndoItemクラス

        #region UndoableContextMenuStripクラス

        private class UndoableContextMenuStrip : ContextMenuStrip
        {
            private readonly TextBox textBox;

            public UndoableContextMenuStrip(TextBox textbox) : base()
            {
                textBox = textbox;

                Items.AddRange(new ToolStripItem[]
                {
                    new ToolStripMenuItem("切り取り", null, (_, __) => textBox.Cut(), Keys.Control | Keys.X),
                    new ToolStripMenuItem("コピー", null, (_, __) => textBox.Copy(), Keys.Control | Keys.C),
                    new ToolStripMenuItem("貼り付け", null, (_, __) => textBox.Paste(), Keys.Control | Keys.V),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem("元に戻す", null, (_, __) => textBox.Undo(), Keys.Control | Keys.Z),
                    new ToolStripMenuItem("やり直し", null, (_, __) => textBox.Redo(), Keys.Control | Keys.Y),
                });
            }

            protected override void OnOpening(CancelEventArgs e)
            {
                base.OnOpening(e);
                if (e.Cancel) return;

                Items[0].Enabled = (textBox.SelectionLength > 0);
                Items[1].Enabled = (textBox.SelectionLength > 0);
                Items[2].Enabled = !string.IsNullOrEmpty(Clipboard.GetText());
                Items[4].Enabled = textBox.CanUndo;
                Items[5].Enabled = textBox.CanRedo;
            }

            protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
            {
                base.OnClosing(e);
                if (e.Cancel) return;

                Items[0].Enabled = true;
                Items[1].Enabled = true;
                Items[2].Enabled = true;
                Items[4].Enabled = true;
                Items[5].Enabled = true;
            }
        }

        #endregion UndoableContextMenuStripクラス
    }
}
