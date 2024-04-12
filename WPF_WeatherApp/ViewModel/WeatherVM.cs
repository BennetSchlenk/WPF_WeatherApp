using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_WeatherApp.Model;
using WPF_WeatherApp.ViewModel.Commands;
using WPF_WeatherApp.ViewModel.Helpers;

namespace WPF_WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities {get; set;}

        private CurrentCondition currentConditions;

        public CurrentCondition CurrentConditions
        {
            get { return currentConditions; }
            set 
            { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            { 
                selectedCity = value;
                if(selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    Name = "Frankfurt am Main",
                };

                CurrentConditions = new CurrentCondition()
                {
                    Current = new Current()
                    {
                        Temp_c = 15,
                        Condition = new Condition()
                        {
                            Text = "partly cloudy"
                        }

                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();

        }



        private void OnPropertyChanged(string propertyName) 
        {
            Debug.WriteLine($"Property change invoke {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery() 
        {
            var cities = await WeatherApiHelper.GetCities(Query);
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditions() 
        {
            Query = string.Empty;
            CurrentConditions = await WeatherApiHelper.GetCurrentCondition(SelectedCity.Name);
            Cities.Clear();

        }
    }
}
