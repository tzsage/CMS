using Common.Code;
using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CMS.Test
{
    [TestClass]
    public class Common
    {
        /// <summary>
        /// 构造依赖注入容器，然后传入参数
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider BuildServiceForSqlServer()
        {
            var services = new ServiceCollection();
            string path = System.IO.Directory.GetCurrentDirectory().Split("bin")[0];
            services.Configure<CodeGenerateOption>(options =>
            {
                options.ConnectionString = "Data Source=.;Initial Catalog=NTCCMS;Integrated Security=True;Max Pool Size=50;Min Pool Size=0;Connection Lifetime=300;";
                options.DbType = DatabaseType.SqlServer.ToString();//数据库类型是SqlServer,其他数据类型参照枚举DatabaseType
                options.Author = "Bale";//作者名称
                options.OutputPath = path.Trim('\\').Substring(0, path.Trim('\\').LastIndexOf('\\'));// "I:\\CmsCodeGenerator";//模板代码生成的路径
                options.ModelsNamespace = "Cms.Models";//实体命名空间
                options.IRepositoryNamespace = "Cms.IRepository";//仓储接口命名空间
                options.RepositoryNamespace = "Cms.Repository";//仓储命名空间
                options.IServicesNamespace = "Cms.IServices";//服务接口命名空间
                options.ServicesNamespace = "Cms.Services";//服务命名空间


            });
            //services.Configure<DbOption>("CzarCms", GetConfiguration().GetSection("DbOpion"));
            //services.AddScoped<IArticleRepository, ArticleRepository>();
            //services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CodeGenerator>();
            return services.BuildServiceProvider(); //构建服务提供程序
        }

        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            return builder.Build();
        }


        /// <summary>
        /// Generate Sql Files
        /// </summary>
        [TestMethod]
        public void GeneratorModelForSqlServer()
        {
            var serviceProvider = Common.BuildServiceForSqlServer();
            var codeGenerator = serviceProvider.GetRequiredService<CodeGenerator>();
            codeGenerator.GenerateTemplateCodesFromDatabase(false);
            Assert.AreEqual("SQLServer", DatabaseType.SqlServer.ToString(), ignoreCase: true);

        }

    }
}
