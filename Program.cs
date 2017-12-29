using System;
using System.Collections.Generic;
using System.IO;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace asiye {
    class Program {
        static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Please enter argument");
                Console.WriteLine("Usage: asiye [--export|--import|--help]");
                return;
            }
            switch (args[0]) {
                case "-h":
                case "help":
                case "--help":
                    Console.WriteLine("Help");
                    break;
                case "-e":
                case "export":
                case "--export":
                    Export();
                    break;
                case "-i":
                case "import":
                case "--import":
                    Console.WriteLine("Not finished!");
                    break;
                default:
                    Console.WriteLine("Invalid argument!");
                    break;
            }
        }

        static void Export() {
            Console.WriteLine("Hello Universe");

            IWebDriver driver = new ChromeDriver();

            LoginYoutube(driver);

            List<string> channels = GetChannelLinks(driver);

            PrintChannelLinks(channels);

            WriteTextFile(channels, "out.txt");

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
        static void WriteTextFile(List<string> ChannelLinks, string filename) {
            using (StreamWriter sw = new StreamWriter(filename)) {

                foreach (string link in ChannelLinks) {
                    sw.WriteLine(link);
                }
            }
        }

        static List<string> ReadTextFile(string filename) {
            List<string> ChannelLinks = new List<string>();
            string line = "";
            using (StreamReader sr = new StreamReader(filename)) {
                while ((line = sr.ReadLine()) != null) {
                    ChannelLinks.Add(line);
                }
            }
            return ChannelLinks;
        }
    }
}
