using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Models
{
    public partial class StoreListing
    {
        public List<StoreListingComment> Comments
        {
            get;
            internal set;
        }
        /// <summary>
        /// The average overall rating given by the reviews for this Store Listing. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public int AverageOverallRating
        {
            get;
            internal set;
        }
        /// <summary>
        /// The last time a review for this Store Listing was revised. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public DateTime LatestReviewRevisionDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The latest time a reviewer for this Store Listing visited or purchased something from this Store. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public DateTime LatestVisitOrPurchaseDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The number of reviews that have been written for this Store Listing. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public int NumberOfReviews
        {
            get;
            internal set;
        }
        /// <summary>
        /// Fills supplementary properties of StoreListing.
        /// </summary>
        public void FillProperties()
        {
            var OverallRatings = new List<int>();
            var RevisionDates = new List<DateTime>();
            var VisitAndPurchaseDates = new List<DateTime>();
            var reviewCount = 0;
            using (var db = new LegatoDataContext())
            {
                Comments = db.StoreListingComments.Where(c => c.ListingID == this.StoreListingID).OrderBy(c => c.DateOfSubmission).ToList();
                foreach (var review in db.StoreReviews.Where(rev => rev.StoreID == StoreListingID))
                {
                    var LatestRevision = db.StoreReviewRevisions.Where(revision => revision.ReviewID == review.ReviewID).OrderByDescending(revision => revision.EditNumber).First();
                    OverallRatings.Add(LatestRevision.RatingOverall);
                    RevisionDates.Add(LatestRevision.RevisionDate);
                    VisitAndPurchaseDates.Add(LatestRevision.DateOfLastPurchase);
                    VisitAndPurchaseDates.Add(LatestRevision.DateOfLastVisit);
                    reviewCount++;
                }
            }
            AverageOverallRating = (int)Math.Round(OverallRatings.Average());
            LatestReviewRevisionDate = RevisionDates.Max();
            LatestVisitOrPurchaseDate = VisitAndPurchaseDates.Max();
            NumberOfReviews = reviewCount;
        }

        /// <summary>
        /// Process a simple search. Accepts AJAX requests from the Google Map on the search page.
        /// </summary>
        /// <param name="box">BoundingBox of the map.</param>
        /// <returns>A List of StoreListings inside the BoundingBox, along with some extra properties that should be displayed in the search results.</returns>
        public static List<StoreListing> ProcessAjaxMapSearch(BoundingBox box)
        {
            using (var db = new LegatoDataContext())
            {
                IQueryable<StoreListing> query = null;

                //Bounding box: latitude processing
                if (box.extent1.latitude <= box.extent2.latitude)
                    query = db.StoreListings.Where(l => l.Lat >= box.extent1.latitude && l.Lat <= box.extent2.latitude);
                else
                    query = db.StoreListings.Where(l => l.Lat >= box.extent2.latitude && l.Lat <= box.extent1.latitude);

                //Bounding box: longitude processing
                if (box.extent1.longitude <= box.extent2.longitude)
                    query = query.Where(l => l.Long >= box.extent1.longitude && l.Long <= box.extent2.longitude);
                else
                    query = query.Where(l => l.Long >= box.extent2.longitude && l.Long <= box.extent1.longitude);
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
    public partial class StoreReview
    {
        public List<StoreReviewComment> Comments
        {
            get;
            set;
        }
        public List<StoreReviewRevision> Revisions
        {
            get;
            set;
        }

        public void FillProperties()
        {
            using (var data = new LegatoDataContext())
            {
                this.Comments = data.StoreReviewComments.Where(c => c.ReviewID == this.ReviewID).OrderBy(c=>c.DateOfSubmission).ToList();
                this.Revisions = data.StoreReviewRevisions.Where(rev => rev.ReviewID == this.ReviewID).OrderByDescending(rev => rev.EditNumber).ToList();
            }
        }

    }
}