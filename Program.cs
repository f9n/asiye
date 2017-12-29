using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace asiye {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello Universe");

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.google.com");

            Console.ReadKey();
            driver.Close();

            Console.WriteLine("Okey!");
        }
    }
}
