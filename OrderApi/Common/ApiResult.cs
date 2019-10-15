using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Common
{
    public class ApiResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 处理结果状态 0表示请求成功，-1表示请求失败
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string errorMsg { get; set; }
    }
}
