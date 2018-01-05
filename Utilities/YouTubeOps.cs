using asiye.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace asiye.Utilities
{
    class YouTubeOps
    {
        public static void Subscribe(IWebDriver driver, Channel channel)
        {
            driver.Navigate().GoToUrl(channel.Url);
            Console.WriteLine($"Started! {channel.Name}");

            driver.FindElement(By.Id("subscribe-button")).Click();

            Console.WriteLine("Finished!");
        }

        public static void Login(IWebDriver driver)
        {
            string message = "Please Login to Youtube, then press any key on console.";

            driver.Navigate().GoToUrl("https://www.youtube.com");

            Console.WriteLine(message);
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript($"alert('{message}');");

            Console.ReadKey();
        }

        public static List<Channel> GetChannels(IWebDriver driver)
        {
            string channels_url = "https://www.youtube.com/feed/channels";
            string classnames = "yt-simple-endpoint style-scope ytd-channel-renderer";
            List<Channel> channels = new List<Channel>();

            driver.Navigate().GoToUrl(channels_url);

            string url, name;
            foreach (IWebElement item in driver.FindElements(By.XPath($"//a[contains(@class, '{classnames}')]")))
            {
                url = item.GetAttribute("href");
                name = item.FindElement(By.TagName("h3")).FindElement(By.TagName("span")).Text;
                Channel tempChannel = new Channel(name, url);
                channels.Add(tempChannel);
            }
            Console.WriteLine(channels.Count);
            return channels;
        }
    }
}
