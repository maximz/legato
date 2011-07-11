using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Legato.Controllers;

namespace Legato.ViewModels
{
    public class SearchResultsModel
    {
        public IOrderedEnumerable<DisplayedResult> Results
        {
            get;
            set;
        }
        public string Query
        {
            get;
            set;
        }
    }
}