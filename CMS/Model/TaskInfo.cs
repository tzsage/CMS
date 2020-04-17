/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：定时任务                                                    
*│　作    者：Bale                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-12-12 15:30:52                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Cms.Models                                  
*│　类    名：TaskInfo                                     
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
	/// 定时任务
	/// </summary>
	public partial class TaskInfo
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String Name {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String Group {get;set;}

		/// <summary>
		///  
		/// </summary>
		[MaxLength(256)]
		public String Description {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(256)]
		public String Assembly {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(256)]
		public String ClassName {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Status {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String Cron {get;set;}

		/// <summary>
		///  
		/// </summary>
		[MaxLength(23)]
		public DateTime? AddTime {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 AddManagerId {get;set;}


	}
}
