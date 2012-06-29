using System.Web.Mvc;
using Omu.Awesome.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Legato.App_Start.MvcProjectAwesome), "Start")]
namespace Legato.App_Start
{    
    public static class MvcProjectAwesome
    {
        public static void Start()
        {
            ModelMetadataProviders.Current = new AwesomeModelMetadataProvider();
        }
    }
}