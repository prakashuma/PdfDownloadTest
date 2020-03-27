using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Linq;
//using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TestPdfDownload
{
    public class PdfDownload
    {
        public static IWebDriver Driver { get; private set; }
        //public static ChromeDriver driver = null;


        [SetUp]
        public void startBrowser()
        {
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:/UmaPrakash/chromedriverwin32/chromedriver.exe");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless", "--window-size=1920,1080");

            var driverService = ChromeDriverService.CreateDefaultService("C:\\UmaPrakash\\chromedriverwin32", "chromedriver.exe");
            Driver = new ChromeDriver(driverService, chromeOptions);
                     

            Task.Run(() => AllowHeadlessDownload(driverService));
            /*var tsTimeout = new TimeSpan(0, 5, 0);
            
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            //chromeOptions.AddUserProfilePreference("download.default_directory", "C:\\UmaPrakash\\TestDownloads");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("plugins.plugins_disabled", "Chrome PDF Viewer");
            chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

            var paramList = new Dictionary<string, object>();
            paramList.Add("behavior", "allow");
            paramList.Add("downloadPath", "C:\\UmaPrakash\\TestDownloads");
            

            driver = new ChromeDriver("C:\\UmaPrakash\\chromedriver_win32\\", chromeOptions, tsTimeout);
            driver.ExecuteChromeCommand("Page.setDownloadBehavior", paramList); */

        }



        [Test]
        public void LaunchUrl()
        {
            System.Console.WriteLine("Into the Test");
            
            Driver.Url = "http://www.seleniumhq.org/download/";
            Driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(20000);
            Driver.FindElement(By.LinkText("32 bit Windows IE")).Click();

            System.Threading.Thread.Sleep(5000);            

        }

        static async Task AllowHeadlessDownload(ChromeDriverService driverService)
        {
            var jsonContent = new JObject(
                new JProperty("cmd", "Page.setDownloadBehavior"),
                new JProperty("params",
                new JObject(new JObject(
                    new JProperty("behavior", "allow"),
                    new JProperty("downloadPath", @"C:\UmaPrakash\TestDownloads")))));
            var content = new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json");
            var sessionIdProperty = typeof(ChromeDriver).GetProperty("SessionId");
            var sessionId = sessionIdProperty.GetValue(Driver, null) as SessionId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = driverService.ServiceUrl;
                var result = await client.PostAsync("session/" + sessionId.ToString() + "/chromium/send_command", content);
                var resultContent = await result.Content.ReadAsStringAsync();
            }
        }
       
       
        [TearDown]
        public void closeBrowser()
        {
             System.Threading.Thread.Sleep(10000);
            // driver.Close();
        }
    }
}
