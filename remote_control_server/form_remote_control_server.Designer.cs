namespace remote_control_server
{
    partial class form_remote_control_server
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_remote_control_server));
            this.ni_remote_control_server = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_ni = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_reserve_internet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_primary_internet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cms_ni.SuspendLayout();
            this.SuspendLayout();
            // 
            // ni_remote_control_server
            // 
            this.ni_remote_control_server.ContextMenuStrip = this.cms_ni;
            this.ni_remote_control_server.Icon = ((System.Drawing.Icon)(resources.GetObject("ni_remote_control_server.Icon")));
            this.ni_remote_control_server.Text = "Remote Control Server";
            this.ni_remote_control_server.Visible = true;
            // 
            // cms_ni
            // 
            this.cms_ni.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_reserve_internet,
            this.tsmi_primary_internet,
            this.toolStripSeparator1,
            this.tsmi_exit});
            this.cms_ni.Name = "cms_ni";
            this.cms_ni.Size = new System.Drawing.Size(192, 98);
            // 
            // tsmi_exit
            // 
            this.tsmi_exit.Name = "tsmi_exit";
            this.tsmi_exit.Size = new System.Drawing.Size(191, 22);
            this.tsmi_exit.Text = "Выход";
            this.tsmi_exit.Click += new System.EventHandler(this.tsmi_exit_Click);
            // 
            // tsmi_reserve_internet
            // 
            this.tsmi_reserve_internet.Name = "tsmi_reserve_internet";
            this.tsmi_reserve_internet.Size = new System.Drawing.Size(191, 22);
            this.tsmi_reserve_internet.Text = "Резервный интернет";
            this.tsmi_reserve_internet.Click += new System.EventHandler(this.tsmi_reserve_internet_Click);
            // 
            // tsmi_primary_internet
            // 
            this.tsmi_primary_internet.Name = "tsmi_primary_internet";
            this.tsmi_primary_internet.Size = new System.Drawing.Size(191, 22);
            this.tsmi_primary_internet.Text = "Основной интернет";
            this.tsmi_primary_internet.Click += new System.EventHandler(this.tsmi_primary_internet_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // form_remote_control_server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "form_remote_control_server";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remote Control Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_remote_control_server_FormClosing);
            this.Load += new System.EventHandler(this.form_remote_control_server_Load);
            this.cms_ni.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon ni_remote_control_server;
        private System.Windows.Forms.ContextMenuStrip cms_ni;
        private System.Windows.Forms.ToolStripMenuItem tsmi_exit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_reserve_internet;
        private System.Windows.Forms.ToolStripMenuItem tsmi_primary_internet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

