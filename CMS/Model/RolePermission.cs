/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：角色权限表                                                    
*│　作    者：Bale                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-12-12 15:30:52                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Cms.Models                                  
*│　类    名：RolePermission                                     
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
	/// 角色权限表
	/// </summary>
	public partial class RolePermission
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 角色主键
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 RoleId {get;set;}

		/// <summary>
		/// 菜单主键
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 MenuId {get;set;}

		/// <summary>
		/// 操作类型（功能权限）
		/// </summary>
		[MaxLength(128)]
		public String Permission {get;set;}


	}
}
