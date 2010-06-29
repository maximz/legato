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
    [OutputCache(Duration = 7200, VaryByParam = "None")] //two hours
    public class CachedContentServeController : Controller
    {
        [Url("static/{id}/png")]
        public ActionResult PngImage(string id)
        {
            return File(MakeContentPath("Content",id,"png"), "image/png");
        }
        [Url("static/{id}/css")]
        public ActionResult StyleSheet(string id)
        {
            return File(MakeContentPath("Content",id, "css"), "text/css");
        }
        [OutputCache(Duration=4838400, VaryByParam="jqueryVersion")] //8 weeks
        [Url("scripts/jQuery/{jqueryVersion}")]
        public ActionResult jQuery(string jqueryVersion)
        {
            return File(MakeContentPath("Scripts","jquery-"+jqueryVersion+".min", "js"), "text/javascript");
        }


        /// <summary>
        /// Creates a path to a Content file (for public ContentServeController methods)
        /// </summary>
        /// <param name="dirName">The name of the directory that the file is in. Usually, for things such as stylesheets and images, it is "Content"; for Javascript and other scripting files, it's "Scripts".</param>
        /// <param name="fileName">The name of the file (without file extension)</param>
        /// <param name="fileExtension">The file extension of the file (with or without a period)</param>
        /// <returns>A string with the combined path.</returns>
        private string MakeContentPath(string dirName, string fileName, string fileExtension)
        {
            var dir = Server.MapPath(dirName);//Server.MapPath("Content");
            var path = Path.Combine(dir, fileName + "."+fileExtension.Replace(".",""));
            return path;
        }

    }
}