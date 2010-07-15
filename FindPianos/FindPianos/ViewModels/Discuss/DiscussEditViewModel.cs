using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.ViewModels.Discuss
{
    public class DiscussEditViewModel : DiscussCreateViewModel
    {
        public bool CanChangeLocation
        {
            get;
            set;
        }
    }
}