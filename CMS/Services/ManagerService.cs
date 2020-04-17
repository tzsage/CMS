/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员                                                    
*│　作    者：Bale                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-12-12 13:56:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Cms.Services                                  
*│　类    名： ManagerService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using AutoMapper;
using Cms.IRepository;
using Cms.IServices;
using Cms.Models;
using Common.Helper;
using Model.ResultModel;
using Model.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Services
{
    public class ManagerService: IManagerService
    {
        private readonly IManagerRepository _repository;
        private readonly IManagerRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IManagerLogRepository _managerLogRepository;

        public ManagerService(IManagerRepository repository, IManagerRoleRepository roleRepository, IMapper mapper, IManagerLogRepository managerLogRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _managerLogRepository = managerLogRepository;
        }

       

        public async Task<BaseResult> DeleteIdsAsync(int[] Ids)
        {
            var result = new BaseResult();
            if (Ids.Count() == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;

            }
            else
            {
                var count = await _repository.DeleteLogicalAsync(Ids);
                if (count > 0)
                {
                    //成功
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    //失败
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }


            }
            return result;
        }

     

        
        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns>状态</returns>
        public async Task<Manager> SignInAsync(LoginModel model)
        {
            model.Password = AESEncryptHelper.Encode(model.Password.Trim(), CmsKeys.AesEncryptKeys);
            model.UserName = model.UserName.Trim();
            string conditions = $"select * from {nameof(Manager)} where IsDelete=0 ";//未删除的
            conditions += $"and (UserName = @UserName or Mobile =@UserName or Email =@UserName) and Password=@Password";
            var manager = await _repository.GetAsync(conditions, model);
            if (manager != null)
            {
                manager.LoginLastIp = model.Ip;
                manager.LoginCount += 1;
                manager.LoginLastTime = DateTime.Now;
                _repository.Update(manager);
                await _managerLogRepository.InsertAsync(new ManagerLog()
                {
                    ActionType = CmsEnums.ActionEnum.SignIn.ToString(),
                    AddManageId = manager.Id,
                    AddManagerNickName = manager.NickName,
                    AddTime = DateTime.Now,
                    AddIp = model.Ip,
                    Remark = "用户登录"
                });
            }
            return manager;
        }

        public async Task<Manager> GetManagerByIdAsync(int id)
        {

            return await _repository.GetAsync(id);
        }

      

       
    }
}