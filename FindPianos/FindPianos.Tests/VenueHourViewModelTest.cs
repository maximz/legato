using FindPianos.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace FindPianos.Tests
{
    
    
    /// <summary>
    ///This is a test class for VenueHourViewModelTest and is intended
    ///to contain all VenueHourViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VenueHourViewModelTest
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
        ///A test for VenueHourViewModel Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void VenueHourViewModelConstructorTest()
        {
            VenueHourViewModel target = new VenueHourViewModel();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Closed
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void ClosedTest()
        {
            VenueHourViewModel target = new VenueHourViewModel(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.Closed = expected;
            actual = target.Closed;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DayOfWeekId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void DayOfWeekIdTest()
        {
            VenueHourViewModel target = new VenueHourViewModel(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.DayOfWeekId = expected;
            actual = target.DayOfWeekId;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DayOfWeekName
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void DayOfWeekNameTest()
        {
            VenueHourViewModel target = new VenueHourViewModel(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.DayOfWeekName = expected;
            actual = target.DayOfWeekName;
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
            VenueHourViewModel target = new VenueHourViewModel(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.EndTime = expected;
            actual = target.EndTime;
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
            VenueHourViewModel target = new VenueHourViewModel(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.StartTime = expected;
            actual = target.StartTime;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
