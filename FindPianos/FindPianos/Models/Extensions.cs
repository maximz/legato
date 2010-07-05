using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Models
{
    public partial class LegatooDataContext
    {
        /// <summary>
        /// Processes a complex SearchForm.
        /// </summary>
        /// <param name="form">The SearchForm in question.</param>
        /// <returns>A List of Listings, with some extra properties that should be displayed on the results page</returns>
        public List<Listing> ProcessSearchForm(SearchForm form)
        {
            using(var db = new LegatoDataContext())
            {
                IQueryable<Listing> query = null;

                //Bounding box: latitude processing
                if (form.bounds.extent1.latitude <= form.bounds.extent2.latitude)
                    query = db.Listings.Where(l => l.Lat >= form.bounds.extent1.latitude && l.Lat <= form.bounds.extent2.latitude);
                else
                    query = db.Listings.Where(l => l.Lat >= form.bounds.extent2.latitude && l.Lat <= form.bounds.extent1.latitude);

                //Bounding box: longitude processing
                if (form.bounds.extent1.longitude <= form.bounds.extent2.longitude)
                    query = query.Where(l => l.Long >= form.bounds.extent1.longitude && l.Long <= form.bounds.extent2.longitude);
                else
                    query = query.Where(l => l.Long >= form.bounds.extent2.longitude && l.Long <= form.bounds.extent1.longitude);
                //Date of Submission (OPTIONAL)
                if (form.startDateSubmission.HasValue && form.endDateSubmission.HasValue)
                {
                    query = query.Where(l => l.DateOfSubmission >= form.startDateSubmission.Value && l.DateOfSubmission <= form.endDateSubmission.Value);
                }
                if (form.pagenumber.HasValue)
                {
                    if (form.pagenumber.Value > 1)
                    {
                        //page 2: skip 25
                        //page 3: skip 50
                        //page 4: skip 75
                        //etcetera
                        query = query.Skip(25*(form.pagenumber.Value-1));
                    }
                }
                query = query.Take(25);


                //Execute query
                var results = query.ToList();

                foreach (var r in results)
                {
                    r.FillProperties();
                }
                return results;
            }
        }
        /// <summary>
        /// Process a simple search. Accepts AJAX requests from the Google Map on the search page.
        /// </summary>
        /// <param name="box">BoundingBox of the map.</param>
        /// <returns>A List of Listings inside the BoundingBox, along with some extra properties that should be displayed in the search results.</returns>
        public List<Listing> ProcessAjaxMapSearch(BoundingBox box)
        {
            using (var db = new LegatoDataContext())
            {
                IQueryable<Listing> query = null;

                //Bounding box: latitude processing
                if (box.extent1.latitude <= box.extent2.latitude)
                    query = db.Listings.Where(l => l.Lat >= box.extent1.latitude && l.Lat <= box.extent2.latitude);
                else
                    query = db.Listings.Where(l => l.Lat >= box.extent2.latitude && l.Lat <= box.extent1.latitude);

                //Bounding box: longitude processing
                if (box.extent1.longitude <= box.extent2.longitude)
                    query = query.Where(l => l.Long >= box.extent1.longitude && l.Long <= box.extent2.longitude);
                else
                    query = query.Where(l => l.Long >= box.extent2.longitude && l.Long <= box.extent1.longitude);
                query = query.Take(25);

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
    public partial class Listing
    {
        public List<ListingComment> Comments
        {
            get;
            internal set;
        }
        /// <summary>
        /// The average overall rating given by the reviews for this Listing. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public int AverageOverallRating
        {
            get;
            internal set;
        }
        /// <summary>
        /// The last time a review for this Listing was revised. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public DateTime LatestReviewRevisionDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The latest time a reviewer for this Listing used the piano. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public DateTime LatestUseOfPianoDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The number of reviews that have been written for this Listing. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public int NumberOfReviews
        {
            get;
            internal set;
        }
        /// <summary>
        /// The average price reviewers of this Listing paid to use this piano, expressed in United States Dollars. This property is filled only when the Listing.FillProperties() method is called.
        /// </summary>
        public double AveragePricePerHourInUSD
        {
            get;
            internal set;
        }
        /// <summary>
        /// Fills supplementary properties of Listing: AverageOverallRating, LatestReviewRevisionDate, LatestUseOfPianoDate, NumberOfReviews, and AveragePricePerHourInUSD.
        /// </summary>
        public void FillProperties()
        {
            var OverallRatings = new List<int>();
            var RevisionDates = new List<DateTime>();
            var UseOfPianoDates = new List<DateTime>();
            var Prices = new List<double>();
            var reviewCount = 0;
            using (var db = new LegatoDataContext())
            {
                Comments = db.ListingComments.Where(c => c.ListingID == this.ListingID).OrderBy(c=>c.CommentID).ToList();
                foreach (var review in db.Reviews.Where(rev => rev.ListingID == ListingID))
                {
                    var LatestRevision = db.ReviewRevisions.Where(revision => revision.ReviewID == review.ReviewID).OrderByDescending(revision => revision.RevisionNumberOfReview).Take(1).ToList()[0];
                    OverallRatings.Add(LatestRevision.RatingOverall);
                    RevisionDates.Add(LatestRevision.DateOfRevision);
                    UseOfPianoDates.Add(LatestRevision.DateOfLastUsageOfPianoBySubmitter);
                    Prices.Add(LatestRevision.PricePerHourInUSD);
                    reviewCount++;
                }
            }
            AverageOverallRating = (int)Math.Round(OverallRatings.Average());
            LatestReviewRevisionDate = RevisionDates.Max();
            LatestUseOfPianoDate = UseOfPianoDates.Max();
            NumberOfReviews = reviewCount;
            AveragePricePerHourInUSD = Prices.Average();
        }
    }
    public partial class Review
    {
        public List<ReviewComment> Comments
        {
            get;
            set;
        }
        public List<ReviewRevision> Revisions
        {
            get;
            set;
        }

        public void FillProperties()
        {
            using (var data = new LegatoDataContext())
            {
                this.Comments = data.ReviewComments.Where(c => c.ReviewID == this.ReviewID).ToList();
                this.Revisions = data.ReviewRevisions.Where(rev => rev.ReviewID == this.ReviewID).OrderByDescending(rev => rev.RevisionNumberOfReview).ToList();
            }
        }

    }
    public class BoundingBox
    {
        public LatLong extent1
        {
            get;
            set;
        }
        public LatLong extent2
        {
            get;
            set;
        }
    }
    public class LatLong
    {
        public decimal latitude
        {
            get;
            set;
        }
        public decimal longitude
        {
            get;
            set;
        }
    }

    public class SearchForm
    {
        public BoundingBox bounds
        {
            get;
            set;
        }
        public DateTime? startDateSubmission
        {
            get;
            set;
        }
        public DateTime? endDateSubmission
        {
            get;
            set;
        }
        public int? pagenumber
        { get; set; }
    }
}