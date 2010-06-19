using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Models
{
    public partial class PianoDataContext
    {
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
        //add properties: AverageOverall, LatestReviewSubmissionDate, LatestUseOfPianoDate, NumberOfReviews
        public int AverageOverallRating
        {
            get;
            set;
        }
        public DateTime LatestReviewSubmissionDate
        {
            get;
            set;
        }
        public DateTime LatestUseOfPianoDate
        {
            get;
            set;
        }
        public int NumberOfReviews
        {
            get;
            set;
        }

        public void FillProperties()
        {
            //add properties: AverageOverall, LatestReviewSubmissionDate, LatestUseOfPianoDate, NumberOfReviews
            var OverallRatings = new List<int>();
            var RevisionDates = new List<DateTime>();
            var UseOfPianoDates = new List<DateTime>();
            var reviewCount = 0;
            using (var db = new PianoDataContext())
            {
                foreach (var review in db.PianoReviews.Where(rev => rev.PianoListingID == PianoID))
                {
                    var LatestRevision = db.PianoReviewRevisions.Where(revision => revision.PianoReviewID == review.PianoReviewID).OrderByDescending(revision => revision.RevisionNumberOfReview).Take(1).ToList()[0];
                    OverallRatings.Add(LatestRevision.RatingOverall);
                    RevisionDates.Add(LatestRevision.DateOfRevision);
                    UseOfPianoDates.Add(LatestRevision.DateOfLastUsageOfPianoBySubmitter);
                    reviewCount++;
                }
            }
            AverageOverallRating = (int)Math.Round(OverallRatings.Average());
            LatestReviewSubmissionDate = RevisionDates.Max();
            LatestUseOfPianoDate = UseOfPianoDates.Max();
            NumberOfReviews = reviewCount;
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