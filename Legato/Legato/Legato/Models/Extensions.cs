using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.Models
{
    public partial class Instrument
    {
        /// <summary>
        /// The average overall rating given by the reviews for this Instrument. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public int AverageOverallRating
        {
            get;
            internal set;
        }
        /// <summary>
        /// The last time a review for this Instrument was revised. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public DateTime LatestReviewRevisionDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The latest time a reviewer for this Instrument used the instrument. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public DateTime LatestUseDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The number of reviews that have been written for this Instrument. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public int NumberOfReviews
        {
            get;
            internal set;
        }

        public void FillProperties()
        {
            var OverallRatings = new List<int>();
            var RevisionDates = new List<DateTime>();
            var UseDates = new List<DateTime>();
            var reviewCount = 0;
            var db = Current.DB;
                foreach (var review in db.InstrumentReviews.Where(rev => rev.InstrumentID == this.InstrumentID))
                {
                    var LatestRevision = db.InstrumentReviewRevisions.Where(revision => revision.ReviewID == review.ReviewID).OrderByDescending(revision => revision.RevisionDate).Take(1).ToList()[0];
                    OverallRatings.Add(LatestRevision.RatingGeneral);
                    RevisionDates.Add(LatestRevision.RevisionDate);
                    UseDates.Add(LatestRevision.LastUseDate);
                    reviewCount++;
                }
            AverageOverallRating = (int)Math.Round(OverallRatings.Average());
            LatestReviewRevisionDate = RevisionDates.Max();
            LatestUseDate = UseDates.Max();
            NumberOfReviews = reviewCount;
        }

    }

    public partial class InstrumentReview
    {
        public List<InstrumentReviewRevision> Revisions
        {
            get;
            internal set;
        }

        public void FillProperties()
        {
            var db = Current.DB;
                this.Revisions = db.InstrumentReviewRevisions.Where(rev => rev.ReviewID == this.ReviewID).OrderByDescending(rev => rev.RevisionDate).ToList();
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
}