using HttpModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;

namespace FindPianos.Tests
{
    
    
    /// <summary>
    ///This is a test class for GlobalGzipTest and is intended
    ///to contain all GlobalGzipTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GlobalGzipTest
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
        ///A test for GlobalGzip Constructor
        ///</summary>
        [TestMethod()]
        public void GlobalGzipConstructorTest()
        {
            GlobalGzip target = new GlobalGzip();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for HandleBeginRequest
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HttpModules.dll")]
        public void HandleBeginRequestTest()
        {
            GlobalGzip_Accessor target = new GlobalGzip_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs evargs = null; // TODO: Initialize to an appropriate value
            target.HandleBeginRequest(sender, evargs);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for System.Web.IHttpModule.Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HttpModules.dll")]
        public void DisposeTest()
        {
            IHttpModule target = new GlobalGzip(); // TODO: Initialize to an appropriate value
            target.Dispose();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for System.Web.IHttpModule.Init
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HttpModules.dll")]
        public void InitTest()
        {
            IHttpModule target = new GlobalGzip(); // TODO: Initialize to an appropriate value
            HttpApplication context = null; // TODO: Initialize to an appropriate value
            target.Init(context);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
