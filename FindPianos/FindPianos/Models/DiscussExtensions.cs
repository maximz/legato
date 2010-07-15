﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Models
{
    public partial class DiscussThread
    {
        /// <summary>
        /// The number of posts that are part of this Discussion Thread, including the original post. This property is filled only when the Thread.FillProperties() method is called.
        /// </summary>
        public int NumberOfPosts
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the first post.
        /// </summary>
        /// <value>The first post.</value>
        public DiscussPost FirstPost
        {
            get;
            internal set;
        }

        /// <summary>
        /// Fills supplementary properties of DiscussThread.
        /// </summary>
        public void FillProperties()
        {
            using (var db = new LegatoDataContext())
            {
                NumberOfPosts = db.DiscussPosts.Where(p => p.ThreadID == this.ThreadID).Count();
                FirstPost = db.DiscussPosts.Where(p => p.ThreadID == this.ThreadID).Where(p => p.PostNumberInThread == 1).Single();
                FirstPost.FillProperties();
            }
        }

        /// <summary>
        /// Process a simple search. Accepts AJAX requests from the Google Map on the search page.
        /// </summary>
        /// <param name="box">BoundingBox of the map.</param>
        /// <returns>A List of DiscussPosts inside the BoundingBox, along with some extra properties that should be displayed in the search results.</returns>
        public static List<DiscussThread> ProcessAjaxMapSearch(BoundingBox box)
        {
            using (var db = new LegatoDataContext())
            {
                IQueryable<DiscussThread> query = null;

                //Bounding box: latitude processing
                if (box.extent1.latitude <= box.extent2.latitude)
                    query = db.DiscussThreads.Where(l => l.Latitude >= box.extent1.latitude && l.Latitude <= box.extent2.latitude);
                else
                    query = db.DiscussThreads.Where(l => l.Latitude >= box.extent2.latitude && l.Latitude <= box.extent1.latitude);

                //Bounding box: longitude processing
                if (box.extent1.longitude <= box.extent2.longitude)
                    query = query.Where(l => l.Longitude >= box.extent1.longitude && l.Longitude <= box.extent2.longitude);
                else
                    query = query.Where(l => l.Longitude >= box.extent2.longitude && l.Longitude <= box.extent1.longitude);
                //query = query.Take(25);

                //Execute query
                var results = query.ToList();

                foreach (var r in results)
                {
                    r.FillProperties();
                }
                return results;

            }
        }
    }
    public partial class DiscussPost
    {
        public List<DiscussPostRevision> Revisions
        {
            get;
            set;
        }
        public void FillProperties()
        {
            using (var data = new LegatoDataContext())
            {
                this.Revisions = data.DiscussPostRevisions.Where(rev => rev.PostID == this.PostID).OrderByDescending(rev => rev.EditNumber).ToList();
            }
        }

    }
}