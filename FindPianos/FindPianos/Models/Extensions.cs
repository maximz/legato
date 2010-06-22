using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Models
{
    public partial class PianoDataContext
    {
        /// <summary>
        /// Processes a complex SearchForm.
        /// </summary>
        /// <param name="form">The SearchForm in question.</param>
        /// <returns>A List of PianoListings, with some extra properties that should be displayed on the results page</returns>
        public List<PianoListing> ProcessSearchForm(SearchForm form)
        {
            using(var db = new PianoDataContext())
            {
                IQueryable<PianoListing> query = null;

                //Bounding box: latitude processing
                if (form.bounds.extent1.latitude <= form.bounds.extent2.latitude)
                    query = db.PianoListings.Where(l => l.Lat >= form.bounds.extent1.latitude && l.Lat <= form.bounds.extent2.latitude);
                else
                    query = db.PianoListings.Where(l => l.Lat >= form.bounds.extent2.latitude && l.Lat <= form.bounds.extent1.latitude);

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
        /// <returns>A List of PianoListings inside the BoundingBox, along with some extra properties that should be displayed in the search results.</returns>
        public List<PianoListing> ProcessAjaxMapSearch(BoundingBox box)
        {
            using (var db = new PianoDataContext())
            {
                IQueryable<PianoListing> query = null;

                //Bounding box: latitude processing
                if (box.extent1.latitude <= box.extent2.latitude)
                    query = db.PianoListings.Where(l => l.Lat >= box.extent1.latitude && l.Lat <= box.extent2.latitude);
                else
                    query = db.PianoListings.Where(l => l.Lat >= box.extent2.latitude && l.Lat <= box.extent1.latitude);

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
    public partial class PianoListing
    {
        public List<PianoListingComment> Comments
        {
            get;
            internal set;
        }
        /// <summary>
        /// The average overall rating given by the reviews for this PianoListing. This property is filled only when the PianoListing.FillProperties() method is called.
        /// </summary>
        public int AverageOverallRating
        {
            get;
            internal set;
        }
        /// <summary>
        /// The last time a review for this PianoListing was revised. This property is filled only when the PianoListing.FillProperties() method is called.
        /// </summary>
        public DateTime LatestReviewRevisionDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The latest time a reviewer for this PianoListing used the piano. This property is filled only when the PianoListing.FillProperties() method is called.
        /// </summary>
        public DateTime LatestUseOfPianoDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The number of reviews that have been written for this PianoListing. This property is filled only when the PianoListing.FillProperties() method is called.
        /// </summary>
        public int NumberOfReviews
        {
            get;
            internal set;
        }
        /// <summary>
        /// The average price reviewers of this PianoListing paid to use this piano, expressed in United States Dollars. This property is filled only when the PianoListing.FillProperties() method is called.
        /// </summary>
        public double AveragePricePerHourInUSD
        {
            get;
            internal set;
        }
        /// <summary>
        /// Fills supplementary properties of PianoListing: AverageOverallRating, LatestReviewRevisionDate, LatestUseOfPianoDate, NumberOfReviews, and AveragePricePerHourInUSD.
        /// </summary>
        public void FillProperties()
        {
            var OverallRatings = new List<int>();
            var RevisionDates = new List<DateTime>();
            var UseOfPianoDates = new List<DateTime>();
            var Prices = new List<double>();
            var reviewCount = 0;
            using (var db = new PianoDataContext())
            {
                Comments = db.PianoListingComments.Where(c => c.PianoListingID == this.PianoID).OrderBy(c=>c.CommentID).ToList();
                foreach (var review in db.PianoReviews.Where(rev => rev.PianoListingID == PianoID))
                {
                    var LatestRevision = db.PianoReviewRevisions.Where(revision => revision.PianoReviewID == review.PianoReviewID).OrderByDescending(revision => revision.RevisionNumberOfReview).Take(1).ToList()[0];
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
    public partial class PianoReview
    {
        public List<PianoReviewComment> Comments
        {
            get;
            set;
        }
        public List<PianoReviewRevision> Revisions
        {
            get;
            set;
        }

        public void FillProperties()
        {
            using (var data = new PianoDataContext())
            {
                this.Comments = data.PianoReviewComments.Where(c => c.PianoReviewID == this.PianoReviewID).ToList();
                this.Revisions = data.PianoReviewRevisions.Where(rev => rev.PianoReviewID == this.PianoReviewID).OrderByDescending(rev => rev.RevisionNumberOfReview).ToList();
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

    public class AjaxFlagContainer
    {
        public long idOfPost
        { get; set; }
        public int flagTypeId
        { get; set; }

    }
}