using Autofac;
using Autofac.Extras.DynamicProxy;
using Cms.Repository;
using Microsoft.Extensions.Options;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace CMSMVC.Models
{
    public class AutofacModuleRegister : Module
    {
        //重写Autofac管道Load方法，在这里注册注入
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ArticleCategoryRepository).Assembly).Where(a => a.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ArticleService).Assembly).Where(a => a.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();

        }
        
    }
}
