using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using RiaLibrary.Web;

namespace FindPianos.Controllers
{
    [HandleError]
    [OutputCache(Duration = 7200, VaryByParam = "None")]
    public class CachedContentServeController : Controller
    {
        [Url("/static/{id}/png")]
        public ActionResult PngImage(string id)
        {
            return File(MakeContentPath(id,"png"), "image/png");
        }
        [Url("/static/{id}/css")]
        public ActionResult StyleSheet(string id)
        {
            return File(MakeContentPath(id, "css"), "text/css");
        }


        /// <summary>
        /// Creates a path to a Content file (for public ContentServeController methods)
        /// </summary>
        /// <param name="id">The name of the file (without file extension)</param>
        /// <param name="extension">The file extension of the file (with or without a period)</param>
        /// <returns>A string with the combined path.</returns>
        private string MakeContentPath(string id, string extension)
        {
            var dir = Server.MapPath("Content");
            var path = Path.Combine(dir, id + "."+extension.Replace(".",""));
            return path;
        }

    }
}