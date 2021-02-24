using log4net;
using log4net.Appender;
using log4net.Core;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SixpenceStudio.AutoUpdate
{
    public partial class AutoUpdate : Form
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();

        public class TextBoxAppender : AppenderSkeleton
        {
            protected override void Append(LoggingEvent loggingEvent)
            {
                StringWriter writer = new StringWriter();
                this.Layout.Format(writer, loggingEvent);
                // 已经得到了按照自己设置的格式的日志消息内容了，就是writer.toString()。然后你想把这句话显示在哪都可以了。。我是测试就直接控制台了。
                Console.Write(writer.ToString());
            }
        }

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
        private List<string> ignoreList = new List<string>() { "log", currentDirectory.Name };

        /// <summary>
        /// 更新文件目录
        /// </summary>
        private string updateFilePath = "";

        public AutoUpdate()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            log = new Log();
            log.Add(new Subscriber() { Name = "日志文本记录", Output = msg => this.loggerTextBox.AppendText(msg) });
        }
        private void InitializeBackgroundWorker()
        {
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgessChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_WorkerCompleted);
        }

        public void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bgWorker.CancellationPending == false)
            {
                ChooseUpdateFile();
                bgWorker.ReportProgress(30, "Working");
                DeleteFolder();
                bgWorker.ReportProgress(60, "Working");
                UncompressFile();
                bgWorker.ReportProgress(100, "Working");
                bgWorker.CancelAsync();
            }
        }

        public void bgWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ProgressProcessBar.Value = e.ProgressPercentage;
            this.progressLabel.Text = Convert.ToString(e.ProgressPercentage) + "%";
        }

        public void bgWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
                return;
            }
        }

        /// <summary>
        /// 获取更新文件
        /// </summary>
        private void ChooseUpdateFile()
        {
            log.Info($"开始获取更新文件");
            using (OpenFileDialog ofg = new OpenFileDialog())
            {
                if (ofg.ShowDialog() == DialogResult.OK)
                {
                    this.updateFilePath = ofg.FileName;
                }
            }
            log.Info($"获取更新文件成功");
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
            log.Info($"更新网站文件成功");
        }

        private void ProgressButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (bgWorker.IsBusy)
                    return;

                this.ProgressProcessBar.Maximum = 100;
                bgWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                log.Error("更新网站出现异常", ex);
                this.ProgressProcessBar.Value = 0;
            }
        }
    }
}
