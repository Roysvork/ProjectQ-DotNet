namespace projectqdotnet
{
    using System;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    using ProjectQ.DotNet;

    [TestFixture]
    public class Test
    {
        private TestingDriver driver;

        public void CleanUp()
        {
            this.driver.Quit();
        }

        private static DesiredCapabilities GetTestObj(DesiredCapabilities cap, string version, string platform)
        {
            cap.SetCapability(CapabilityType.Platform, platform);
            cap.SetCapability(CapabilityType.Version, version);
            cap.IsJavaScriptEnabled = true;
            cap.SetCapability("screenshot", true);
            cap.SetCapability("name", "test name");

            return cap;
        }

        private void DoTitleTest(ICapabilities cap)
        {
            this.driver = new TestingDriver(cap);
            this.driver.Navigate().GoToUrl("http://www.google.com");

            Assert.That(this.driver.Title, Is.EqualTo("Google"));
        }

        [Test]
        public void Win_InternetExplorer_9()
        {
            var cap = GetTestObj(DesiredCapabilities.InternetExplorer(), "9", "WINDOWS");
            this.DoTitleTest(cap);
        }

        [Test]
        public void Win_InternetExplorer_10()
        {
            var cap = GetTestObj(DesiredCapabilities.InternetExplorer(), "10", "WINDOWS");
            this.DoTitleTest(cap);
        }
    }
}