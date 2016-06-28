using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class WeatherData
    {
        public WeatherData()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("Loading Information");
            Console.WriteLine("-------------------\n");
        }

        public void dataFetch()
        {
            string url = "http://openweathermap.org/city/611717";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);



            string city = doc.DocumentNode.SelectNodes("//html//body//div//div//div//div//div//div//h3")[0].InnerText;
            string temp = doc.DocumentNode.SelectNodes("//html//body//div//div//div//div//div//div//h2//text()")[1].InnerText;
            string Humidity = doc.DocumentNode.SelectNodes("/html/body/div[4]/div[2]/div[1]/div/div[1]/div/table/tbody/tr[4]/td[2]")[0].InnerText;


            Console.WriteLine($"City: {city}");
            Console.WriteLine($"Temp: {temp}");
            Console.WriteLine($"Humidty: {Humidity}");

            var tempConv = temp;
            var HumidConv = Humidity;

            tempConv = tempConv.Trim('°', 'C', 'c', ',', ' ');
            tempConv = tempConv.Trim();
            HumidConv = HumidConv.Trim(' ', '%');



            float numeric = float.Parse(tempConv);
            float humidNom = float.Parse(HumidConv);

            GetReading(numeric);

        }

        private static void GetReading(float numeric)
        {


            if (numeric > 15 && numeric < 20)
            {
                Console.WriteLine("No need for extra heating");
            }
            else if (numeric < 15 && numeric > 5)
            {
                Console.WriteLine("Getting chilly");
            }
            else if (numeric < 5)
            {
                Console.WriteLine("Turn on the heat");
            }
            else if (numeric > 20 && numeric < 30)
            {
                Console.WriteLine("Turn off the ongoing heating");
            }
            else if (numeric > 30 && numeric < 40)
            {
                Console.WriteLine("Activating Cooling");
            }
            else
            {
                Console.WriteLine("Full cooling mode");
            }

        }


    }
}
