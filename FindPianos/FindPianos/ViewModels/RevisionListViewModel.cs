using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Models;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
{
    public class RevisionListViewModel
    {
        [Required(ErrorMessage="The revision list is required.")]
        public List<ReviewRevision> Revisions
        {
            get;
            set;
        }
    }
}