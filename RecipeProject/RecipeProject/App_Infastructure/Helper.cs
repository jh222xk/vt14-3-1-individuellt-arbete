using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeProject.App_Infastructure
{
    public static class Helper
    {
        public static IEnumerable<T> ExceptWhere<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            return source.Where(x => !predicate(x));
        }
    }
}