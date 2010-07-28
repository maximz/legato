using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Models;
using FindPianos.Helpers;

namespace FindPianos.ViewModels
{
    public class DiscussReadThreadViewModel
    {
        public DiscussThread Thread
        {
            get;
            set;
        }
        public PagedList<DiscussPost> Posts
        {
            get;
            set;
        }

        public PageNumber PageNumbers
        {
            get;
            set;
        }

        public string TotalPosts
        {
            get;
            set;
        }

        public string BoardName
        {
            get;
            set;
        }

        public long BoardID
        {
            get;
            set;
        }
    }
}