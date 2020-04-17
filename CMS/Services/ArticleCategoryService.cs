/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类                                                    
*│　作    者：Bale                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-12-12 13:56:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Cms.Services                                  
*│　类    名： ArticleCategoryService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Cms.IRepository;
using Cms.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Services
{
    public class ArticleCategoryService: IArticleCategoryService
    {
        private readonly IArticleCategoryRepository _repository;
	
        public ArticleCategoryService(IArticleCategoryRepository repository)
        {
            _repository = repository;
        }
    }
}