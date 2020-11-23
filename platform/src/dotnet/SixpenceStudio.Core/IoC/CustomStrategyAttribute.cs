#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/23 20:31:00
Description：自定义策略类特性
********************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.IoC
{
    /// <summary>
    /// 策略类特性（所有策略接口都应该打上该标记）
    /// </summary>
    public class CustomStrategyAttribute : Attribute
    {
    }
}
