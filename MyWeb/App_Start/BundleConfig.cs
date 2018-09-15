using System.Web;
using System.Web.Optimization;

namespace MyWeb
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //捆绑压缩js
            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                        "~/js/common.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/js/jquery-{version}.js",
                        "~/js/index.js"
                        ));

            //捆绑压缩css
            bundles.Add(new StyleBundle("~/bundles/commoncss").Include(
                      "~/css/common.css"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/css/font-awesome.css",
                      "~/css/index.css"));
        }
    }
}
