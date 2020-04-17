using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class StatusModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 修改后的状态
        /// </summary>
        public Boolean Status { get; set; }
    }
}
