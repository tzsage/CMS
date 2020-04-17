/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：操作日志                                                    
*│　作    者：Bale                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-12-12 15:30:52                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Cms.Models                                  
*│　类    名：ManagerLog                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Models
{
	/// <summary>
	/// Bale
	/// 2019-12-12 15:30:52
	/// 操作日志
	/// </summary>
	public partial class ManagerLog
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 操作类型
		/// </summary>
		[MaxLength(32)]
		public String ActionType {get;set;}

		/// <summary>
		/// 主键
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 AddManageId {get;set;}

		/// <summary>
		/// 操作人名称
		/// </summary>
		[MaxLength(64)]
		public String AddManagerNickName {get;set;}

		/// <summary>
		/// 操作时间
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}

		/// <summary>
		/// 操作IP
		/// </summary>
		[MaxLength(64)]
		public String AddIp {get;set;}

		/// <summary>
		/// 备注
		/// </summary>
		[MaxLength(256)]
		public String Remark {get;set;}


	}
}
