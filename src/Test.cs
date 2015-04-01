namespace projectqdotnet
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium.Remote;

    [TestClass]
    public class UnitTest1
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
                return this.testContextInstance;
            }
            set
            {
                this.testContextInstance = value;
            }
        }


        private TestingBotDriver driver;

        [TestCleanup]
        public void CleanUp()
        {
            String sessionId = this.driver.getSessionId();
            bool success = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            try
            {
                this.driver.updateTestStatus(sessionId, success, this.TestContext.FullyQualifiedTestClassName + " " + DateTime.Now.ToLongTimeString() + " " + this.TestContext.CurrentTestOutcome.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.driver.Quit();
            }
        }

        /// <param name="cap">TestObj</param>
        /// <param name="version">browser version</param>
        /// <param name="platform">OS</param>
        /// <returns></returns>
        private DesiredCapabilities GetTestObj(DesiredCapabilities cap, string version, string platform)
        {
            cap.SetCapability(CapabilityType.Platform, platform);
            cap.SetCapability(CapabilityType.Version, version);
            cap.IsJavaScriptEnabled = true;
            cap.SetCapability("screenshot", true);
            cap.SetCapability("name", "test name");
            cap.SetCapability("api_key", TestingBotDriver.APIKEY);
            cap.SetCapability("api_secret", TestingBotDriver.APISECRET);

            return cap;
        }

        /// <param name="cap"></param>
        private void DoTitleTest(DesiredCapabilities cap)
        {
            this.driver = new TestingBotDriver(new Uri("http://hub.testingbot.com:80/wd/hub/"), cap);
            this.driver.Navigate().GoToUrl("http://www.google.com");

            StringAssert.Equals(this.driver.Title, "Google");
        }

        [TestMethod]
        public void Win_InternetExplorer_7()
        {
            DesiredCapabilities cap = this.GetTestObj(DesiredCapabilities.InternetExplorer(), "7", "WINDOWS");
            this.DoTitleTest(cap);
        }

        [TestMethod]
        public void Win_InternetExplorer_8()
        {
            DesiredCapabilities cap = this.GetTestObj(DesiredCapabilities.InternetExplorer(), "8", "WINDOWS");
            this.DoTitleTest(cap);
        }

        [TestMethod]
        public void Win_InternetExplorer_9()
        {
            DesiredCapabilities cap = this.GetTestObj(DesiredCapabilities.InternetExplorer(), "9", "WINDOWS");
            this.DoTitleTest(cap);
        }
    }
}