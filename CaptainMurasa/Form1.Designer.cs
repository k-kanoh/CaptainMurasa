namespace CaptainMurasa
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainImage = new CaptainMurasa.PictureBox();
            this.CaptureList = new CaptainMurasa.ComboBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainImage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1160, 703);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.MainImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1154, 677);
            this.panel1.TabIndex = 1;
            // 
            // MainImage
            // 
            this.MainImage.Location = new System.Drawing.Point(0, 0);
            this.MainImage.Margin = new System.Windows.Forms.Padding(0);
            this.MainImage.Name = "MainImage";
            this.MainImage.Size = new System.Drawing.Size(1154, 711);
            this.MainImage.TabIndex = 0;
            this.MainImage.TabStop = false;
            // 
            // CaptureList
            // 
            this.CaptureList.DisplayMember = "Name";
            this.CaptureList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CaptureList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CaptureList.FormattingEnabled = true;
            this.CaptureList.IntegralHeight = false;
            this.CaptureList.Location = new System.Drawing.Point(861, 12);
            this.CaptureList.MaxDropDownItems = 13;
            this.CaptureList.Name = "CaptureList";
            this.CaptureList.Size = new System.Drawing.Size(311, 28);
            this.CaptureList.TabIndex = 1;
            this.CaptureList.ValueMember = "Code";
            this.CaptureList.SelectedIndexChanged += new System.EventHandler(this.CaptureList_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.CaptureList);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private PictureBox MainImage;
        private ComboBox CaptureList;
    }
}

