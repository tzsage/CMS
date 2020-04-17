/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：角色权限表                                                    
*│　作    者：Bale                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-12-12 13:56:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Cms.Services                                  
*│　类    名： RolePermissionService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Cms.IRepository;
using Cms.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Services
{
    public class RolePermissionService: IRolePermissionService
    {
        private readonly IRolePermissionRepository _repository;
		public RolePermissionService()
		{
		}
        public RolePermissionService(IRolePermissionRepository repository)
        {
            _repository = repository;
        }
    }
}