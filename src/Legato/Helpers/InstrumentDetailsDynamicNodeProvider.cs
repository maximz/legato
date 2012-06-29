using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider.Extensibility;
using Legato.Models;
using System.Web.Caching;

namespace Legato.Helpers
{
    public class InstrumentDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        LegatoDataContext db = Current.DB;

        /// <summary>
        /// Gets the dynamic node collection.
        /// </summary>
        /// <returns>
        /// A dynamic node collection represented.
        /// </returns>
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            // Create a node for each album
            foreach (var ins in db.Instruments)
            {
                ins.FillProperties();
                DynamicNode node = new DynamicNode();
                node.Title = ins.Title;
                node.Controller = "Instruments";
                node.Action = "Individual";
                //node.ParentKey = "Type_" + ins.InstrumentType.Name;
                node.Key = "Instrument_" + ins.InstrumentID.ToString();
                node.RouteValues.Add("instrumentID", ins.InstrumentID);
                node.RouteValues.Add("slug", ins.UrlSlug);

                yield return node;
            }
        }

        /// <summary>
        /// Gets a cache description for the dynamic node collection 
        /// or null if there is none.
        /// </summary>
        /// <returns>
        /// A cache description represented as a <see cref="CacheDescription"/> instance .
        /// </returns>
        public override CacheDescription GetCacheDescription()
        {
            return new CacheDescription("InstrumentDetailsDynamicNodeProvider")
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }
    }
}