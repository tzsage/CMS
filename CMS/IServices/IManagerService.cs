/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员                                                    
*│　作    者：Bale                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-12-12 13:56:55                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Cms.IServices                                   
*│　接口名称： IManagerRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Cms.Models;
using Model.ViewsModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.IServices
{
    public interface IManagerService
    {

        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns>实体对象</returns>
        Task<Manager> SignInAsync(LoginModel model);
    }
}