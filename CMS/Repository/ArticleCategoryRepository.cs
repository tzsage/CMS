/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类接口实现                                                    
*│　作    者：Bale                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-12-12 13:56:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Cms.Repository                                  
*│　类    名： ArticleCategoryRepository                                      
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
    public class ArticleCategoryRepository:BaseRepository<ArticleCategory,Int32>, IArticleCategoryRepository
    {
	
        public ArticleCategoryRepository(IOptionsSnapshot<DbOption> options)
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
            string sql = "update ArticleCategory set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update ArticleCategory set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

    }
}