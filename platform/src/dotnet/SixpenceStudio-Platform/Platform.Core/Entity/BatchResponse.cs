using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Core.Entity
{
    public class BatchResponse
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }

        public List<BaseResponse<string>> Data { get; set; } = new List<BaseResponse<string>>();
    }

    public class BaseResponse<T>
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; }
    }
}
