using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using WPF_WeatherApp.Model;

namespace WPF_WeatherApp.ViewModel.Helpers
{
    internal class WeatherApiHelper
    {
        public const string Base_Url = "https://api.weatherapi.com/v1/";
        public const string AutocompleteEndpoint = "search.json?key={0}&q={1}";
        public const string CurrentEndpoint = "current.json?key={0}&q={1}&aqi={2}";


        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            string url = Base_Url + string.Format(AutocompleteEndpoint, App.Key, query); 

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities.AddRange(JsonConvert.DeserializeObject<IEnumerable<City>>(json)??Array.Empty<City>());
            }

            return cities;
        }

        public static async Task<CurrentCondition> GetCurrentCondition(string cityName) 
        {
            CurrentCondition condition = new CurrentCondition();

            string url = Base_Url + string.Format(CurrentEndpoint, App.Key, cityName, "yes");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode == false) 
                {
                   return null;
                }

                string json = await response.Content.ReadAsStringAsync();

                condition = JsonConvert.DeserializeObject<CurrentCondition>(json)!;
            }

            return condition;

        }
    }
}
