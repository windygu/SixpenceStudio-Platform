
namespace SixpenceStudio.AutoUpdate
{
    partial class AutoUpdate
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ProgressProcessBar = new Sunny.UI.UIProcessBar();
            this.ProgressButton = new Sunny.UI.UIButton();
            this.progressLabel = new Sunny.UI.UILabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.loggerTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressProcessBar
            // 
            this.ProgressProcessBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ProgressProcessBar.Location = new System.Drawing.Point(12, 30);
            this.ProgressProcessBar.MinimumSize = new System.Drawing.Size(70, 5);
            this.ProgressProcessBar.Name = "ProgressProcessBar";
            this.ProgressProcessBar.Size = new System.Drawing.Size(686, 29);
            this.ProgressProcessBar.TabIndex = 0;
            this.ProgressProcessBar.Text = "0.0%";
            // 
            // ProgressButton
            // 
            this.ProgressButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProgressButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ProgressButton.Location = new System.Drawing.Point(791, 20);
            this.ProgressButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.ProgressButton.Name = "ProgressButton";
            this.ProgressButton.Size = new System.Drawing.Size(107, 45);
            this.ProgressButton.TabIndex = 1;
            this.ProgressButton.Text = "Start";
            this.ProgressButton.Click += new System.EventHandler(this.ProgressButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.progressLabel.Location = new System.Drawing.Point(704, 30);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(81, 23);
            this.progressLabel.TabIndex = 2;
            this.progressLabel.Text = "0%";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ProgressProcessBar);
            this.panel1.Controls.Add(this.progressLabel);
            this.panel1.Controls.Add(this.ProgressButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 82);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.loggerTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(910, 465);
            this.panel2.TabIndex = 5;
            // 
            // loggerTextBox
            // 
            this.loggerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerTextBox.Location = new System.Drawing.Point(0, 0);
            this.loggerTextBox.Multiline = true;
            this.loggerTextBox.Name = "loggerTextBox";
            this.loggerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.loggerTextBox.Size = new System.Drawing.Size(910, 465);
            this.loggerTextBox.TabIndex = 0;
            // 
            // AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 547);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AutoUpdate";
            this.Text = "自动更新";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIProcessBar ProgressProcessBar;
        private Sunny.UI.UIButton ProgressButton;
        private Sunny.UI.UILabel progressLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox loggerTextBox;
    }
}

