using NUnit.Framework;
using UnityEngine;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    public class SeleniumTest
    {
        [Test]
        public void SeleniumTestSimplePasses()
        {
            var path = Application.streamingAssetsPath;
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            using (ChromeDriver driver = new ChromeDriver(path, options))
            {
                driver.Navigate().GoToUrl("https://yahoo.co.jp");
                driver.FindElementByXPath("//*[@id=\"tabTopics2\"]/a").Click();
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
