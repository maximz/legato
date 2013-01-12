using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.Models
{
    /// <summary>
    /// Various methods for computing a sort score for a post (depending on what information is available).
    /// </summary>
    public class Rankings
    {
        #region upvote-downvote-algorithm

        /// <summary>
        /// Computes lower bound of Wilson confidence interval at 95% confidence. This is the method used by Reddit for its "best" ranking/sorting of comments (thanks to Randall Munroe).
        /// Explanations (in order from earliest to latest, and we use the latest below): http://blog.reddit.com/2009/10/reddits-new-comment-sorting-system.html, http://amix.dk/blog/post/19588, http://possiblywrong.wordpress.com/2011/06/05/reddits-comment-ranking-algorithm/
        /// </summary>
        /// <param name="ups">Number of upvotes.</param>
        /// <param name="downs">Number of downvotes.</param>
        /// <returns>Weighted rank (sort number)</returns>
        public double wilson_confidence(int ups, int downs)
        {
            /* Python source:
                from math import sqrt

                def confidence_fixed(ups, downs):
                    if ups == 0:
                        return -downs
                    n = ups + downs
                    z = 1.64485 #1.0 = 85%, 1.6 = 95%
                    phat = float(ups) / n
                    return (phat+z*z/(2*n)-z*sqrt((phat*(1-phat)+z*z/(4*n))/n))/(1+z*z/n)
             * 
             * */

            if (ups == 0)
            {
                return -downs;
            }
            var n = ups + downs;
            var z = 1.64485; // #1.0 = 85%, 1.6 = 95% (from normal distribution)
            var phat = ((double)ups) / n;
            return (phat + z * z / (2 * n) - z * Math.Sqrt((phat * (1 - phat) + z * z / (4 * n)) / n)) / (1 + z * z / n);
        }

        #endregion

        #region star-ratings-algorithms

        /// <summary>
        /// Computes lower bound of Wilson confidence interval at 95% confidence. This is an adaptation of Wilson confidence interval (meant for upvotes and downvotes) to more elaborate star ratings, from 1 to k stars.
        /// From: http://www.goproblems.com/test/wilson/wilson.php?v1=0&v2=0&v3=3&v4=0&v5=0 and http://www.talkstats.com/showthread.php/18200-Confidence-Intervals-How-to-determine-for-5-star-ratings.
        /// It's unclear whether this is a valid approach: http://stats.stackexchange.com/questions/15979/how-to-find-confidence-intervals-for-ratings.
        /// </summary>
        /// <param name="votes">Array of star ratings as integers, e.g. [1, 5, 2, 3, 4, 5].</param>
        /// <returns>Weighted rank (sort number)</returns>
        public double wilson_confidence_star_ratings(int[] votes)
        {
            var phat = (votes.Average() - 1) /4; // xbar
            var n = votes.Length;
            var z = 1.64485; // #1.0 = 85%, 1.6 = 95% (from normal distribution)
            var score = (phat + z * z / (2 * n) - z * Math.Sqrt((phat * (1 - phat) + z * z / (4 * n)) / n)) / (1 + z * z / n);
            return 1 + 4 * score;
        }

        /// <summary>
        /// From http://stats.stackexchange.com/questions/15979/how-to-find-confidence-intervals-for-ratings:
        /// Why might using confidence intervals not work too well? One reason is that if you don't have many ratings for an item, then your confidence interval is going to be very wide, so the lower bound of the confidence interval will be small. Thus, items without many ratings will end up at the bottom of your list.
        /// Intuitively, however, you probably want items without many ratings to be near the average item, so you want to wiggle your estimated rating of the item toward the mean rating over all items (i.e., you want to push your estimated rating toward a prior). This is exactly what a Bayesian approach does.
        /// </summary>
        /// <param name="votes">Array of star ratings as integers, e.g. [1, 5, 2, 3, 4, 5].</param>
        /// <returns>Weighted rank (sort number)</returns>
        public double bayesian_normal_distribution_star_ratings(int[] votes)
        {
            var rating_mean = votes.Average();
            var n = votes.Length;
            var prior = 3; // mean over all items or whatever you want to shrink your rating to
            var threshold = 2; // minimum number of review required to be listed
            var weight = n / (n + threshold);

            return weight * rating_mean + (1 - weight) * prior;
        }

        #endregion
    }
}