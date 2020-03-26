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

namespace TestPdfDownload
{
    public class PdfDownload
    {
        public static IWebDriver driver;
        [SetUp]
        public void startBrowser()
        {
            var tsTimeout = new TimeSpan(0, 5, 0);

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", "C:\\UmaPrakash\\TestDownloads");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("plugins.plugins_disabled", "Chrome PDF Viewer");
            chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            driver = new ChromeDriver("C:\\UmaPrakash\\chromedriver_win32\\", chromeOptions, tsTimeout);


        }

        [Test]
        public void LaunchUrl()
        {
            driver.Url = "https://www.guru99.com/selenium-tutorial-pdf.html";
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(20000);

            String title = driver.Title;
            Assert.AreEqual(title, "Selenium Tutorial PDF");

            driver.FindElement(By.XPath("//p[3]//a[1]//img[1]")).Click();
        }
       
        [TearDown]
        public void closeBrowser()
        {
            // System.Threading.Thread.Sleep(10000);
            // driver.Close();
        }
    }
}
