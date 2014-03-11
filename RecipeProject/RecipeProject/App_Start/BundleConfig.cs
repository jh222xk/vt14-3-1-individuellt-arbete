using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RecipeProject
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/base.css",
                "~/Content/recipe.css"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/recipe.js"
            ));
        }
    }
}