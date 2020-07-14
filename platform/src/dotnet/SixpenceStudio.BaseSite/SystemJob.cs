using SixpenceStudio.BaseSite.SysConfig.Config;
using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite
{
    /// <summary>
    /// 系统作业（Daily）
    /// </summary>
    public class SystemJob : JobBase
    {
        public override string Name => "系统作业";

        public override string CronExperssion => "0 0 12 * * ?";

        public override string Description => "清理系统无效文件及整理日志";

        public override void Run(IPersistBroker broker)
        {
            DeletePictures(broker);
            ArchiveLog();
        }

        /// <summary>
        /// 删除缓存照片
        /// </summary>
        /// <param name="broker"></param>
        private void DeletePictures(IPersistBroker broker)
        {
            var sql = @"
SELECT
	* 
FROM
	sys_file 
WHERE
	objectid IS NULL 
	AND (file_type = 'blog_content' OR file_type = 'blog_surface')
";
            var dataList = broker.RetrieveMultiple<sys_file>(sql);
            var ids = dataList.Select(item => item.Id).ToList();
            new SysFileService().DeleteData(ids);
        }

        /// <summary>
        /// 归档日志
        /// </summary>
        private void ArchiveLog()
        {
            var fileList = FileUtils.GetFileList("*.log", FolderType.log).Where(item => !item.Contains(DateTime.Now.ToString("yyyyMMdd"))).ToList();
            var targetPath = FileUtils.GetSystemPath(FolderType.logArchive);
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            FileUtils.MoveFiles(fileList, targetPath);
            DeleteLog();

        }

        /// <summary>
        /// 删除归档里的log
        /// </summary>
        private void DeleteLog()
        {
            var days = ConfigUtils.GetValue<BackupLogConfig>()?.ToString();
            var path = FileUtils.GetSystemPath(FolderType.logArchive);
            var files = FileUtils.GetFileList(path);
            var logNameList = new List<string>();

            // 需要保留的log
            for (int i = 0; i < Convert.ToInt32(days); i++)
            {
                logNameList.Add(DateTime.Now.AddDays(-i).ToString("yyyyMMdd") + " debug.log");
                logNameList.Add(DateTime.Now.AddDays(-i).ToString("yyyyMMdd") + " error.log");
            }

            // 删除不需要保留的log
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (!logNameList.Contains(file))
                {
                    FileUtils.DeleteFile(file);
                }
            }
        }
    }
}