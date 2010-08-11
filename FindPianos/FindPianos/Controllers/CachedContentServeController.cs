using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using RiaLibrary.Web;
using FindPianos.Helpers;

namespace FindPianos.Controllers
{
    /// <summary>
    /// Caches content.
    /// </summary>
    [HandleError]
    public class CachedContentServeController : CustomControllerBase
    {
        /// <summary>
        /// Returns PNG images in the Content directory.
        /// </summary>
        /// <param name="id">The file name (without .png).</param>
        /// <returns></returns>
        [Url("Content/{id}/png")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "id")] //two hours
        public ActionResult PngImage(string id)
        {
            return File(MakeContentPath("Content",id,"png"), "image/png");
        }
        /// <summary>
        /// Returns CSS files in the Content directory.
        /// </summary>
        /// <param name="id">The file name (without .css).</param>
        /// <returns></returns>
        [Url("Content/{id}/css")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "id")] //two hours
        public ActionResult StyleSheet(string id)
        {
            return File(MakeContentPath("Content",id, "css"), "text/css");
        }
        /// <summary>
        /// Opens the content of the ID.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        [Url("Content/openid/{Name}",Order=1)]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration=7200,VaryByParam="Name")] //two hours
        public ActionResult OpenIDContent(string Name)
        {
            if(Name.IsNullOrEmpty())
            {
                return RedirectToAction("NotFound", "Error");
            }
            var id = Name.Trim();
            var extension = "";
            var name = "";

            if(!id.Contains("."))
            {
                name = id;
                extension = "";
            }
            else
            {
                var lastDot = id.LastIndexOf(".");
                name = id.Substring(0, lastDot);
                extension = id.Substring(lastDot);
            }
            var mimeType = "text/plain";
            if (extension == ".css" || extension == "css")
            {
                mimeType = "text/css";
            }
            else if(extension.HasValue())
            {
                mimeType = "image/" + extension.Replace(".", "");
            }
            return File(MakeContentPath("Content/openid", name, extension), mimeType);
        }
        /// <summary>
        /// Returns a version of jQuery.min.js.
        /// </summary>
        /// <param name="jqueryVersion">The requested version of jQuery.</param>
        /// <returns>The file.</returns>
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration=4838400, VaryByParam="jqueryVersion")] //8 weeks
        [Url("Scripts/jQuery/{jqueryVersion}")]
        public ActionResult jQuery(string jqueryVersion)
        {
            return File(MakeContentPath("Scripts","jquery-"+jqueryVersion+".min", "js"), "text/javascript");
        }
        /// <summary>
        /// Returns a version of jQueryUI.min.js.
        /// </summary>
        /// <param name="jqueryVersion">The requested version of jQuery UI.</param>
        /// <returns>The file.</returns>
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 4838400, VaryByParam = "jqueryVersion")] //8 weeks
        [Url("Scripts/jQueryUI/{jqueryUIVersion}")]
        public ActionResult jQueryUI(string jqueryUIVersion)
        {
            return File(MakeContentPath("Scripts", "jquery-ui-" + jqueryUIVersion + ".min", "js"), "text/javascript");
        }
        /// <summary>
        /// Gets a JavaScript script by name; cached for 8 weeks.
        /// </summary>
        /// <param name="name">The name of the file, without its extension.</param>
        /// <returns>The file.</returns>
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 4838400, VaryByParam = "*")] //8 weeks
        [Url("Scripts/js/{name}")]
        public ActionResult getSomeJavascript(string name)
        {
            return File(MakeContentPath("Scripts", name, "js"), "text/javascript");
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
            //var dir = Server.MapPath("../../" + dirName);//Server.MapPath("Content");
            var dir = Server.MapPath("~/" + dirName);
            var path = Path.Combine(dir, fileName + "."+fileExtension.Replace(".",""));
            return path;
        }

    }
}