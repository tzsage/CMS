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
        /// ��������ע��������Ȼ�������
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider BuildServiceForSqlServer()
        {
            var services = new ServiceCollection();
            string path = System.IO.Directory.GetCurrentDirectory().Split("bin")[0];
            services.Configure<CodeGenerateOption>(options =>
            {
                options.ConnectionString = "Data Source=.;Initial Catalog=NTCCMS;Integrated Security=True;Max Pool Size=50;Min Pool Size=0;Connection Lifetime=300;";
                options.DbType = DatabaseType.SqlServer.ToString();//���ݿ�������SqlServer,�����������Ͳ���ö��DatabaseType
                options.Author = "Bale";//��������
                options.OutputPath = path.Trim('\\').Substring(0, path.Trim('\\').LastIndexOf('\\'));// "I:\\CmsCodeGenerator";//ģ��������ɵ�·��
                options.ModelsNamespace = "Cms.Models";//ʵ�������ռ�
                options.IRepositoryNamespace = "Cms.IRepository";//�ִ��ӿ������ռ�
                options.RepositoryNamespace = "Cms.Repository";//�ִ������ռ�
                options.IServicesNamespace = "Cms.IServices";//����ӿ������ռ�
                options.ServicesNamespace = "Cms.Services";//���������ռ�


            });
            //services.Configure<DbOption>("CzarCms", GetConfiguration().GetSection("DbOpion"));
            //services.AddScoped<IArticleRepository, ArticleRepository>();
            //services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CodeGenerator>();
            return services.BuildServiceProvider(); //���������ṩ����
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
