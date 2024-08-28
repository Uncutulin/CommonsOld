using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Commons.HtmlHelpers
{
    public static class StringHtmlHelpers
    {
        public static string Truncate(this IHtmlHelper helper, string texto, int numeroCaracteres)
        {
            if (texto == null)
            {
                return String.Empty;
            }
            if (texto.Length <= numeroCaracteres)
            {
                return texto;
            }
            else
            {
                return texto.Substring(0, numeroCaracteres) + "...";
            }
        }

        public static string Cuil(this IHtmlHelper helper, int cuilInteger)
        {
            return Cuil(null, cuilInteger.ToString());
        }

        public static string Cuil(this IHtmlHelper helper, string cuilString)
        {
            string start = cuilString.Substring(0, 2);
            string middle = cuilString.Substring(2, cuilString.Length - 3);
            string end = cuilString.Substring(cuilString.Length - 1, 1);

            return start + "-" + middle + "-" + end;
        }
    }
}