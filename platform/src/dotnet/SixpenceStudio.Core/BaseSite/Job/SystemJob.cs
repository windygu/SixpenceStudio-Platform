using Quartz;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.SysConfig;
using SixpenceStudio.Core.SysConfig.Config;
using SixpenceStudio.Core.SysFile;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SixpenceStudio.Core.Job
{
    /// <summary>
    /// 系统作业（Daily）
    /// </summary>
    public class SystemJob : JobBase
    {
        public override string Name => "系统作业";

        public override IScheduleBuilder ScheduleBuilder => CronScheduleBuilder.CronSchedule("0 0 4 * * ?");

        public override string Description => "清理系统无效文件及整理日志";
        public override void Executing(IJobExecutionContext context)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
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
            try
            {
                var fileList = FileUtil.GetFileList("*.log", FolderType.log, SearchOption.TopDirectoryOnly).Where(item => !item.Contains(DateTime.Now.ToString("yyyyMMdd"))).ToList();
                var targetPath = FileUtil.GetSystemPath(FolderType.logArchive);
                FileUtil.MoveFiles(fileList, targetPath);
                DeleteLog();
            }
            catch (Exception e)
            {
                Logger.Error("日志归档出现异常", e);
                throw e;
            }
        }

        /// <summary>
        /// 删除归档里的log
        /// </summary>
        private void DeleteLog()
        {
            var days = SysConfigFactory.GetValue<BackupLogSysConfig>();
            var files = FileUtil.GetFileList("*.log", FolderType.logArchive);
            var logNameList = new List<string>();

            // 需要保留的log
            for (int i = 0; i < Convert.ToInt32(days); i++)
            {
                logNameList.Add(DateTime.Now.AddDays(-i).ToString("yyyyMMdd"));
            }

            // 删除不需要保留的log
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (logNameList.Count(Item => Path.GetFileName(file).Contains(Item)) == 0)
                {
                    FileUtil.DeleteFile(file);
                }
            }
        }
    }
}