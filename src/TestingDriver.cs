namespace ProjectQ.DotNet
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TestingDriver : RemoteWebDriver
    {
        public TestingDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
        }

        public TestingDriver(ICapabilities desiredCapabilities)
            : base(new Uri("http://alphaprojectq.cloudapp.net:4444/wd/hub"), desiredCapabilities)
        {
        }

        public TestingDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities, TimeSpan.FromSeconds(120))
        {
        }

        public String getSessionId()
        {
            return this.SessionId.ToString();
        }
    }
}