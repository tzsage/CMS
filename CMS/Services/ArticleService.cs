using Cms.Models;
using IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ArticleService : IArticleService
    {
        /// <summary>
        /// 查询文章
        /// </summary>
        /// <returns></returns>
        public Article GetArticleById(int Id)
        {

            return new Article();
        }
    }
}
