using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Commons.HtmlHelpers
{
    public static class ImageHtmlHelpers
    {
        public static string ObtenerImagenDeByte(this IHtmlHelper helper, byte[] streamBytes)
        {
            string imgSrc = String.Empty;

            try
            {
                var base64 = Convert.ToBase64String(streamBytes);
                imgSrc = $"data:image/gif;base64,{base64}";

            }
            catch (Exception BYTE_NULL_OR_ERROR)
            {
                Console.WriteLine(BYTE_NULL_OR_ERROR);
                imgSrc = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRxQ-XnyKmKP11SQacp2IutD8Njt89ze2EIIN9AAtrEP5g83jI";
            }


            return imgSrc;
        }
    }
}