﻿using FindPianos.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Data.Linq;

namespace FindPianos.Tests
{
    
    
    /// <summary>
    ///This is a test class for aspnet_ProfileTest and is intended
    ///to contain all aspnet_ProfileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class aspnet_ProfileTest
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
        ///A test for aspnet_Profile Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void aspnet_ProfileConstructorTest()
        {
            aspnet_Profile target = new aspnet_Profile();
            Assert.Inconclusive("TODO: Implement code to verify target");
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
            aspnet_Profile_Accessor target = new aspnet_Profile_Accessor(); // TODO: Initialize to an appropriate value
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
            aspnet_Profile_Accessor target = new aspnet_Profile_Accessor(); // TODO: Initialize to an appropriate value
            target.SendPropertyChanging();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for LastUpdatedDate
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void LastUpdatedDateTest()
        {
            aspnet_Profile target = new aspnet_Profile(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.LastUpdatedDate = expected;
            actual = target.LastUpdatedDate;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PropertyNames
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void PropertyNamesTest()
        {
            aspnet_Profile target = new aspnet_Profile(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.PropertyNames = expected;
            actual = target.PropertyNames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PropertyValuesBinary
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void PropertyValuesBinaryTest()
        {
            aspnet_Profile target = new aspnet_Profile(); // TODO: Initialize to an appropriate value
            Binary expected = null; // TODO: Initialize to an appropriate value
            Binary actual;
            target.PropertyValuesBinary = expected;
            actual = target.PropertyValuesBinary;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PropertyValuesString
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void PropertyValuesStringTest()
        {
            aspnet_Profile target = new aspnet_Profile(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.PropertyValuesString = expected;
            actual = target.PropertyValuesString;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UserId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void UserIdTest()
        {
            aspnet_Profile target = new aspnet_Profile(); // TODO: Initialize to an appropriate value
            Guid expected = new Guid(); // TODO: Initialize to an appropriate value
            Guid actual;
            target.UserId = expected;
            actual = target.UserId;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for aspnet_User
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Ilya\\Documents\\My Dropbox\\Summer Projects\\Repos\\findpianos\\FindPianos\\FindPianos", "/")]
        [UrlToTest("http://localhost:2574/")]
        public void aspnet_UserTest()
        {
            aspnet_Profile target = new aspnet_Profile(); // TODO: Initialize to an appropriate value
            aspnet_User expected = null; // TODO: Initialize to an appropriate value
            aspnet_User actual;
            target.aspnet_User = expected;
            actual = target.aspnet_User;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}