using System;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace asiye {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello Universe");

            IWebDriver driver = new ChromeDriver();

            LoginYoutube(driver);

            List<string> channels = GetChannelLinks(driver);

            PrintChannelLinks(channels);

            Console.ReadKey();
            driver.Close();

            Console.WriteLine("Okey!");
        }

        static void LoginYoutube(IWebDriver driver) {
            string message = "Please Login to Youtube, then press any key on console.";

            driver.Navigate().GoToUrl("https://www.youtube.com");

            Console.WriteLine(message);
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript($"alert('{message}');");

            Console.ReadKey();
        }

        static List<string> GetChannelLinks(IWebDriver driver) {
            string channels_url = "https://www.youtube.com/feed/channels";
            string classnames = "yt-simple-endpoint style-scope ytd-channel-renderer";
            List<string> ChannelLinks = new List<string>();

            driver.Navigate().GoToUrl(channels_url);
            string temp;
            foreach(var item in driver.FindElements(By.XPath($"//a[contains(@class, '{classnames}')]"))) {
                temp = item.GetAttribute("href");
                ChannelLinks.Add(temp);
            }
            Console.WriteLine(ChannelLinks.Count);
            return ChannelLinks;
        }

        static void PrintChannelLinks(List<string> ChannelLinks) {
            foreach(var link in ChannelLinks) {
                Console.WriteLine(link);
            }
        }
    }
}
