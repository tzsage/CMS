﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSMVC.Filter
{

    public class StaticPageAttribute : ActionFilterAttribute
    {
        public string Key
        {
            get; set;
        }

        /// <summary>
        /// Action 执行完毕 生成文件
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (string.IsNullOrEmpty(Key))
                return;
            //获取结果
            IActionResult actionResult = context.Result;
            //判断结果是否是一个ViewResult
            if (actionResult is ViewResult)
            {
                ViewResult viewResult = actionResult as ViewResult;
                //下面的代码就是执行这个ViewResult，并把结果的html内容放到一个StringBuiler对象中
                var services = context.HttpContext.RequestServices;
                var executor = services.GetRequiredService<IActionResultExecutor<ViewResult>>() as ViewResultExecutor?? throw new ArgumentNullException("executor");// services.GetRequiredService<ViewResultExecutor>();
                var option = services.GetRequiredService<IOptions<MvcViewOptions>>();
                var result = executor.FindView(context, viewResult);
                result.EnsureSuccessful(originalLocations: null);
                var view = result.View;
                StringBuilder builder = new StringBuilder();

                using (var writer = new StringWriter(builder))
                {
                    var viewContext = new ViewContext(
                        context,
                        view,
                        viewResult.ViewData,
                        viewResult.TempData,
                        writer,
                        option.Value.HtmlHelperOptions);

                    view.RenderAsync(viewContext).GetAwaiter().GetResult();
                    //这句一定要调用，否则内容就会是空的
                    writer.Flush();
                }
                //按照规则生成静态文件名称
                string area = (context.RouteData.Values["area"]?.ToString().ToLower())??"sitehome";
                string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
                string actionName = context.RouteData.Values["action"].ToString().ToLower();
                string id = context.RouteData.Values.ContainsKey(Key) ? context.RouteData.Values[Key].ToString() : "";
                if (string.IsNullOrEmpty(id) && context.HttpContext.Request.Query.ContainsKey(Key))
                {
                    id = context.HttpContext.Request.Query[Key];
                }
                string devicedir = Path.Combine(AppContext.BaseDirectory, "wwwroot", area);
                if (!Directory.Exists(devicedir))
                {
                    Directory.CreateDirectory(devicedir);
                }

                //写入文件
                string filePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", area, controllerName + "-" + actionName + (string.IsNullOrEmpty(id) ? "" : ("-" + id)) + ".html");
                using (FileStream fs = File.Open(filePath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(builder.ToString());
                    }
                }
                //输出当前的结果
                ContentResult contentresult = new ContentResult();
                contentresult.Content = builder.ToString();
                contentresult.ContentType = "text/html";
                context.Result = contentresult;
            }
        }

        /// <summary>
        /// 请求前 判断静态文件
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                if (string.IsNullOrEmpty(Key))
                    return;
                //按照一定的规则生成静态文件的名称，这里是按照area+"-"+controller+"-"+action+key规则生成
                string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
                string actionName = context.RouteData.Values["action"].ToString().ToLower();
                string area = (context.RouteData.Values["area"]?.ToString().ToLower())??"sitehome";
                //这里的Key默认等于id，当然我们可以配置不同的Key名称
                string id = context.RouteData.Values.ContainsKey(Key) ? context.RouteData.Values[Key].ToString() : "";
                if (string.IsNullOrEmpty(id) && context.HttpContext.Request.Query.ContainsKey(Key))
                {
                    id = context.HttpContext.Request.Query[Key];
                }
                string filePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", area, controllerName + "-" + actionName + (string.IsNullOrEmpty(id) ? "" : ("-" + id)) + ".html");

                //获取文件信息对象
                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    TimeSpan ts = DateTime.Now - fileInfo.CreationTime;
                    if (ts.TotalMinutes <= 120)
                    {
                        //如果存在，直接读取文件
                        using (FileStream fs = File.Open(filePath, FileMode.Open))
                        {
                            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                            {
                                //通过contentresult返回文件内容
                                ContentResult contentresult = new ContentResult();
                                contentresult.Content = sr.ReadToEnd();
                                contentresult.ContentType = "text/html";
                                context.Result = contentresult;
                            }
                        }
                    }
                }

            }

        }

       

    }
}
