﻿using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysFile
{
    [RequestAuthorize]
    public class SysFileController : EntityController<sys_file, SysFileService>
    {
    }
}