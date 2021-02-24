
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressProcessBar
            // 
            this.ProgressProcessBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ProgressProcessBar.Location = new System.Drawing.Point(9, 24);
            this.ProgressProcessBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgressProcessBar.MinimumSize = new System.Drawing.Size(52, 4);
            this.ProgressProcessBar.Name = "ProgressProcessBar";
            this.ProgressProcessBar.Size = new System.Drawing.Size(387, 23);
            this.ProgressProcessBar.TabIndex = 0;
            this.ProgressProcessBar.Text = "0.0%";
            // 
            // ProgressButton
            // 
            this.ProgressButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProgressButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ProgressButton.Location = new System.Drawing.Point(593, 16);
            this.ProgressButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgressButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.ProgressButton.Name = "ProgressButton";
            this.ProgressButton.Size = new System.Drawing.Size(80, 36);
            this.ProgressButton.TabIndex = 1;
            this.ProgressButton.Text = "Start";
            this.ProgressButton.Click += new System.EventHandler(this.ProgressButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.progressLabel.Location = new System.Drawing.Point(400, 24);
            this.progressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(61, 18);
            this.progressLabel.TabIndex = 2;
            this.progressLabel.Text = "0%";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.ProgressProcessBar);
            this.panel1.Controls.Add(this.progressLabel);
            this.panel1.Controls.Add(this.ProgressButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 66);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.loggerTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 66);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(682, 372);
            this.panel2.TabIndex = 5;
            // 
            // loggerTextBox
            // 
            this.loggerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerTextBox.Location = new System.Drawing.Point(0, 0);
            this.loggerTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loggerTextBox.Multiline = true;
            this.loggerTextBox.Name = "loggerTextBox";
            this.loggerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.loggerTextBox.Size = new System.Drawing.Size(682, 372);
            this.loggerTextBox.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(468, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "成功后删除更新包";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 438);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AutoUpdate";
            this.Text = "自动更新";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

