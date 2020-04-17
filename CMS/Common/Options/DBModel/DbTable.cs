﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Options
{
    [Serializable]
    public class DbTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表说明
        /// </summary>
        public string TableComment { get; set; }
        /// <summary>
        /// 字段集合
        /// </summary>
        public virtual List<DbTableColumn> Columns { get; set; } = new List<DbTableColumn>();
    }
}
