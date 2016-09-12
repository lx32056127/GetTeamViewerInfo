namespace GetTeamViewerInfo.View
{
    partial class ShowInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvData = new System.Windows.Forms.ListView();
            this.chId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPwd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvData
            // 
            this.lvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chPwd,
            this.chAddr});
            this.lvData.Location = new System.Drawing.Point(12, 12);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(304, 286);
            this.lvData.TabIndex = 0;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // chId
            // 
            this.chId.Text = "TeamViewerID";
            this.chId.Width = 100;
            // 
            // chPwd
            // 
            this.chPwd.Text = "TeamViewer密码";
            this.chPwd.Width = 100;
            // 
            // chAddr
            // 
            this.chAddr.Text = "所在位置";
            this.chAddr.Width = 100;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(241, 304);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "刷新";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // ShowInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 330);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.lvData);
            this.Name = "ShowInfo";
            this.Text = "TeamViewer信息列表";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chPwd;
        private System.Windows.Forms.ColumnHeader chAddr;
        private System.Windows.Forms.Button btnReload;
    }
}