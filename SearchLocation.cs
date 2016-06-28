using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Weather
{
    class SearchLocation
    {
        public string location { get; set; }

        public SearchLocation()
        {
            string loc;
            do
            {
                Console.WriteLine("\n===================");
                Console.Write("Enter Location: ");

                loc = Console.ReadLine();
                if (loc == "exit")
                {
                    Environment.Exit(0);
                }
                else if (loc.Equals(string.Empty))
                {
                    Console.WriteLine("Enter valid City");

                }
                else if (loc == "hot")
                {
                    most();
                }

                location = loc;
                location = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(location); //Capitalization
            } while (loc.Equals(string.Empty) || loc == "hot");


        }

        public void searchLocation()
        {


            int finalID = 0;
            var locationID = "http://openweathermap.org/help/city_list.txt";

            WebClient dataDown = new WebClient();
            var ID = new List<string>();
            string rawID = dataDown.DownloadString(locationID);
            ID = rawID.Split('\n').ToList();
            var pattern = @"^\d+\s\w+";
            var pat2 = @"^\d+";
            Regex patID = new Regex(pat2);
            Regex pat = new Regex(pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < ID.Count; i++)
            {
                ID[i] = pat.Match(ID[i]).ToString();
            }



            foreach (var item in ID)
            {
                if (item.Contains(location))
                {
                    finalID = int.Parse(patID.Match(item).ToString());
                    //Console.WriteLine(finalID);
                }

            }

            GetStats(finalID);

        }

        private static void GetStats(int finalID)
        {
            HtmlWeb web = new HtmlWeb();
            var link = "http://openweathermap.org/city/" + finalID;
            HtmlDocument doc = web.Load(link);

            string city = doc.DocumentNode.SelectNodes("html/body/div[4]/div[2]/div[1]/div/div[1]/div/h3")[0].InnerText;
            string temp = doc.DocumentNode.SelectNodes("//html//body//div//div//div//div//div//div//h2//text()")[1].InnerText;
            string Humidity = doc.DocumentNode.SelectNodes("/html/body/div[4]/div[2]/div[1]/div/div[1]/div/table/tbody/tr[4]/td[2]")[0].InnerText;


            Console.WriteLine($"City: {city}");
            Console.WriteLine($"Temp: {temp}");
            Console.WriteLine($"Humidty: {Humidity}");
        }

        public void most()
        {
            HtmlWeb web = new HtmlWeb();
            var link = "http://www.timeanddate.com/weather/?sort=6";

            HtmlDocument doc = web.Load(link);

            string mostTemp = doc.DocumentNode.SelectNodes("//html//body//div//div//section//div//table//tr//td[4]")[0].InnerText;
            string cityHot = doc.DocumentNode.SelectNodes("//html/body//div//div//section//div//table//tr//td[1]//a")[0].InnerText;



            string pat = @"\d+";
            Regex reg = new Regex(pat);
            Console.WriteLine(cityHot + ": " + reg.Match(mostTemp) + "°C");




        }

    }



}

