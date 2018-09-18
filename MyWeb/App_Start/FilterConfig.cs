using System.Web;
using System.Web.Mvc;

namespace MyWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //异常过滤器
            filters.Add(new Log4netExceptionFilterAttribute());

            //Action登陆权限过滤器
            //filters.Add(new LoginCheckAttribute());

        }
    }
}
