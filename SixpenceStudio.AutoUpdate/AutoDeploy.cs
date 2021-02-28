using log4net;
using log4net.Appender;
using log4net.Core;
using SixpenceStudio.Core;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SixpenceStudio.AutoUpdate
{
    public partial class AutoDeploy : Form
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private Log log;

        /// <summary>
        /// 网站目录地址
        /// </summary>
        private string webPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf(@"\"));

        /// <summary>
        /// 当前活动目录
        /// </summary>
        private static DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        /// <summary>
        /// 忽略文件夹
        /// </summary>
        private List<string> ignoreList = new List<string>()
        {
            "log",
            currentDirectory.Name
        };

        /// <summary>
        /// 更新文件目录
        /// </summary>
        private string updateFilePath = "";

        public AutoDeploy()
        {
            InitializeComponent();
            log = new Log();
            log.Add(new Subscriber()
            {
                Name = "日志文本记录",
                Output = msg => Invoke(new Action<string>(data => this.loggerTextBox.AppendText(data)), msg)
            });
            this.checkBox1.Checked = true;
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        private void GetCoreConfig()
        {
            var sourcePath = Path.Combine(webPath, "bin", "Core.config");
            var destPath = Path.Combine(Application.StartupPath, "Core.config");
            FileUtil.CopyFile(sourcePath, destPath);
            log.Info("获取Core.config配置文件成功");
        }

        private void GetWebConfig()
        {
            var sourcePath = Path.Combine(webPath, "Web.config");
            var destPath = Path.Combine(Application.StartupPath, "Web.config");
            FileUtil.CopyFile(sourcePath, destPath);
            log.Info("获取Web.config配置文件成功");
        }

        /// <summary>
        /// 复制配置文件
        /// </summary>
        private void CopyCoreConfig()
        {
            var destPath = Path.Combine(webPath, "bin", "Core.config");
            var sourcePath = Path.Combine(Application.StartupPath, "Core.config");
            FileUtil.CopyFile(sourcePath, destPath);
        }

        private void CopyWebConfig()
        {
            var sourcePath = Path.Combine(Application.StartupPath, "Web.config");
            var destPath = Path.Combine(webPath, "Web.config");
            FileUtil.CopyFile(sourcePath, destPath);
        }

        /// <summary>
        /// 获取更新文件
        /// </summary>
        private bool ChooseUpdateFile()
        {
            log.Info($"开始获取更新文件");
            using (OpenFileDialog ofg = new OpenFileDialog())
            {
                ofg.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf(@"\"));
                if (ofg.ShowDialog() == DialogResult.OK)
                {
                    this.updateFilePath = ofg.FileName;
                    this.ignoreList.Add(ofg.SafeFileName);
                    log.Info($"获取更新文件成功");
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 删除网站文件
        /// </summary>
        private void DeleteFolder()
        {
            log.Info($"开始删除网站文件");
            FileUtil.DeleteFolder(webPath, ignoreList);
            log.Info($"删除网站文件成功");
        }

        /// <summary>
        /// 更新网站文件
        /// </summary>
        private void UncompressFile()
        {
            log.Info($"开始更新网站文件");
            ZipFile.ExtractToDirectory(updateFilePath, webPath);
            if (checkBox1.Checked)
            {
                FileUtil.DeleteFile(updateFilePath);
                log.Info("删除更新文件成功");
            }
            log.Info($"更新网站文件成功");
        }

        private void ProgressButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ProgressProcessBar.Maximum = 100;
                GetCoreConfig();
                GetWebConfig();
                var result = ChooseUpdateFile();
                if (result)
                {
                    this.ProgressProcessBar.Value = 30;
                    this.progressLabel.Text = "30%";
                    DeleteFolder();
                    this.ProgressProcessBar.Value = 60;
                    this.progressLabel.Text = "60%";
                    UncompressFile();
                    CopyCoreConfig();
                    CopyWebConfig();
                    this.ProgressProcessBar.Value = 100;
                    this.progressLabel.Text = "已完成";
                }
                else
                {
                    log.Info("已取消");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                this.ProgressProcessBar.Value = 0;
            }
        }
    }
}
