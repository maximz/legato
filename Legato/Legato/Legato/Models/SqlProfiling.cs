using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Legato.Models
{
    /// <summary>
    /// This partial class is for MvcMiniProfiler, so that SQL queries are also profiled. See http://code.google.com/p/mvc-mini-profiler/ and http://code.google.com/p/stack-exchange-data-explorer/source/browse/App/StackExchange.DataExplorer/Models/DBContext.cs
    /// </summary>
    public partial class LegatoDataContext
    {
        /// <summary>
        /// Answers a new DBContext for the current site.
        /// </summary>
        public static LegatoDataContext GetContext()
        {
            var cnnString = ConfigurationManager.ConnectionStrings["legatoConnectionString"].ConnectionString;
            return new LegatoDataContext(MvcMiniProfiler.Data.ProfiledDbConnection.Get(new SqlConnection(cnnString)));
        }

    }
}