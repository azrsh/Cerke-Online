using NUnit.Framework;
using UnityEngine;
using OpenQA.Selenium.Chrome;

namespace Libraries.Tests
{
    public class SeleniumTest
    {
        [Test]
        public void SeleniumTestSimplePasses()
        {
            var path = Application.dataPath + "/Libraries/Selenium";
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            using (ChromeDriver driver = new ChromeDriver(path, options))
            {
                driver.Navigate().GoToUrl("https://yahoo.co.jp");
                driver.FindElementByXPath("//*[@id=\"tabTopics2\"]/a").Click();
            }
        }
    }
}
