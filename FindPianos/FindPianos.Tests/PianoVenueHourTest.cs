using FindPianos.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Data.Linq;

namespace FindPianos.Tests
{
    
    
    /// <summary>
    ///This is a test class for PianoVenueHourTest and is intended
    ///to contain all PianoVenueHourTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PianoVenueHourTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for PianoVenueHour Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void PianoVenueHourConstructorTest()
        {
            PianoVenueHour target = new PianoVenueHour();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetRuleViolations
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void GetRuleViolationsTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            IEnumerable<RuleViolation> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<RuleViolation> actual;
            actual = target.GetRuleViolations();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OnValidate
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        [DeploymentItem("FindPianos.dll")]
        public void OnValidateTest()
        {
            PianoVenueHour_Accessor target = new PianoVenueHour_Accessor(); // TODO: Initialize to an appropriate value
            ChangeAction action = new ChangeAction(); // TODO: Initialize to an appropriate value
            target.OnValidate(action);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SendPropertyChanged
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        [DeploymentItem("FindPianos.dll")]
        public void SendPropertyChangedTest()
        {
            PianoVenueHour_Accessor target = new PianoVenueHour_Accessor(); // TODO: Initialize to an appropriate value
            string propertyName = string.Empty; // TODO: Initialize to an appropriate value
            target.SendPropertyChanged(propertyName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SendPropertyChanging
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        [DeploymentItem("FindPianos.dll")]
        public void SendPropertyChangingTest()
        {
            PianoVenueHour_Accessor target = new PianoVenueHour_Accessor(); // TODO: Initialize to an appropriate value
            target.SendPropertyChanging();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DayOfWeek
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void DayOfWeekTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.DayOfWeek = expected;
            actual = target.DayOfWeek;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EndTime
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void EndTimeTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.EndTime = expected;
            actual = target.EndTime;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void IsValidTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsValid;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PianoReviewRevision
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void PianoReviewRevisionTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            PianoReviewRevision expected = null; // TODO: Initialize to an appropriate value
            PianoReviewRevision actual;
            target.PianoReviewRevision = expected;
            actual = target.PianoReviewRevision;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReviewRevisionID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void ReviewRevisionIDTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            long expected = 0; // TODO: Initialize to an appropriate value
            long actual;
            target.ReviewRevisionID = expected;
            actual = target.ReviewRevisionID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StartTime
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void StartTimeTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.StartTime = expected;
            actual = target.StartTime;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for VenueHoursID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void VenueHoursIDTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            long expected = 0; // TODO: Initialize to an appropriate value
            long actual;
            target.VenueHoursID = expected;
            actual = target.VenueHoursID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WeekDay
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void WeekDayTest()
        {
            PianoVenueHour target = new PianoVenueHour(); // TODO: Initialize to an appropriate value
            WeekDay expected = null; // TODO: Initialize to an appropriate value
            WeekDay actual;
            target.WeekDay = expected;
            actual = target.WeekDay;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
