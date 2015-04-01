//using Selenium;  

namespace testingbotdemo
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TestingBotDriver : RemoteWebDriver
    {

        public static string APIKEY = "key";
        public static string APISECRET = "secret";

        public TestingBotDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
        }

        public TestingBotDriver(ICapabilities desiredCapabilities)
            : base(new Uri("http://hub.testingbot.com:80/wd/hub"), desiredCapabilities)
        {
        }

        public TestingBotDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities, TimeSpan.FromSeconds(120))
        {
        }

        public String getSessionId()
        {
            return this.SessionId.ToString();
        }

        public void updateTestStatus(String sessionId, bool success, String testName)
        {
            String url = "http://api.testingbot.com/v1/tests/" + sessionId;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "PUT";
            string username = APIKEY;
            string password = APISECRET;

            string usernamePassword = username + ":" + password;
            CredentialCache mycache = new CredentialCache();
            mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
            request.Credentials = mycache;
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write("test[success]=" + (success ? "1" : "0") + "&test[name]=" + testName);
            }

            WebResponse response = request.GetResponse();
            response.Close();
        }
    }
}