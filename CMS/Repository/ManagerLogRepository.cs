/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：操作日志接口实现                                                    
*│　作    者：Bale                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-12-12 13:56:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Cms.Repository                                  
*│　类    名： ManagerLogRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Common.Options;
using Cms.IRepository;
using Cms.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Cms.Repository
{
    public class ManagerLogRepository:BaseRepository<ManagerLog,Int32>, IManagerLogRepository
    {
		public ManagerLogRepository()
		{}
        public ManagerLogRepository(IOptionsSnapshot<DbOption> options)
        {
            _dbOption =options.Get("Cms");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

		public int DeleteLogical(int[] ids)
        {
            string sql = "update ManagerLog set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update ManagerLog set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

    }
}