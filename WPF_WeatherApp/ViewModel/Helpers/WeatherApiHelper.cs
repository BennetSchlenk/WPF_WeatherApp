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
        public const string Base_Url = "http://api.weatherapi.com/v1/";
        public const string AutocompleteEndpoint = "search.json?key={0}&q={1}";
        public const string CurrentEndpoint = "current.json?key={0}&q={1}&aqi={2}";

        public static async Task<List<City>> GetCities(string query) 
        {
            List<City> cities = new List<City>();

            string url = Base_Url + string.Format(AutocompleteEndpoint, Key, query); 

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentCondition> GetCurrentCondition(string cityName) 
        {
            CurrentCondition condition = new CurrentCondition();

            string url = Base_Url + string.Format(CurrentEndpoint, Key, cityName, "yes");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                condition = JsonConvert.DeserializeObject<CurrentCondition>(json);
            }

            return condition;

        }
    }
}
