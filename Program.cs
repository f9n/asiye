using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using asiye.Models;
using asiye.Utilities;

namespace asiye {
    class Program {
        static void Main(string[] args) {

            //TODO: Argüman alma kodu ile asıl işlemin yapılacağı kod ayrılacak.

            string filename = "out.json";

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
                    if(args.Length == 2) {
                        filename = args[1];
                    }
                    Export(filename);
                    break;
                case "-i":
                case "import":
                case "--import":
                    if(args.Length == 2) {
                        filename = args[1];
                    } else {
                        Console.WriteLine("Please enter argument like this,");
                        Console.WriteLine("asiye --import subscription-list.json");
                        return;
                    }
                    Import(filename);
                    break;
                default:
                    Console.WriteLine("Invalid argument!");
                    break;
            }
        }

        public static void Import(string filename) {
            List<Channel> Channels = ReadWriteOps.ReadJsonFile(filename);

            IWebDriver driver = new ChromeDriver();

            YouTubeOps.Login(driver);

            foreach (Channel channel in Channels) {
                YouTubeOps.Subscribe(driver, channel);
            }

            Console.ReadKey();
            driver.Close();
        }

        public static void Export(string filename) {
            Console.WriteLine("Hello Universe");

            IWebDriver driver = new ChromeDriver();

            YouTubeOps.Login(driver);

            List<Channel> Channels = YouTubeOps.GetChannels(driver);

            PrintChannels(Channels);

            //WriteTextFile(channels, "out.txt");
            ReadWriteOps.WriteJsonFile(Channels, filename);

            Console.ReadKey();
            driver.Close();

            Console.WriteLine("Okey!");
        }

        public static void PrintChannels(List<Channel> channels) {

            foreach (Channel channel in channels) {
                Console.WriteLine($"{channel.Name} {channel.Url}");
            }
        }
    }
}
