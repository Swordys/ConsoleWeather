using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;



namespace Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            var get = new WeatherData();
            get.dataFetch();

            while (true)
            {
                var setloc = new SearchLocation();
               
                setloc.searchLocation();
                

            }

        }
    }
}
