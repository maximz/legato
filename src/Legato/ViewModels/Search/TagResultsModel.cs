using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Legato.Controllers;

namespace Legato.ViewModels
{
    public class TagResultsModel : SearchResultsModel
    {
        public List<string> Tags
        {
            get;
            set;
        }
    }
}