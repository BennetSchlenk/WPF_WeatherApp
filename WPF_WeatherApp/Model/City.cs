using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_WeatherApp.Model
{
    internal class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Url { get; set; }
    }
}
