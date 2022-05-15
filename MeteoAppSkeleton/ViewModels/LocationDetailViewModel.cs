using MeteoAppSkeleton.Models;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MeteoAppSkeleton.ViewModels
{
   public class LocationDetailViewModel : BaseViewModel
   {
      Location _location;

      public Location Location
      {
         get { return _location; }
         set 
         { 
            _location = value;
            OnPropertyChanged();
         }
      }

      public LocationDetailViewModel(Location location)
      {
         Location = location;
         _ = getWeatherAsync();
      }

      private async Task getWeatherAsync()
      {
         var httpClient = new HttpClient();
         var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + Location.Name + "&appid=bda36c03a06ee5e92467b7ccb9ecbacc");

         string weatherCurrent = (string)JObject.Parse(content)["main"]["temp"];
         string weatherMax = (string)JObject.Parse(content)["main"]["temp_max"];
         string weatherMin = (string)JObject.Parse(content)["main"]["temp_min"];
         string weatherIcon = (string)JObject.Parse(content)["weather"][0]["icon"];

         CurrentTemperature = ConvertKelvinToC(Convert.ToDouble(weatherCurrent));
         MinimumTemperature = ConvertKelvinToC(Convert.ToDouble(weatherMin));
         MaximumTemperature = ConvertKelvinToC(Convert.ToDouble(weatherMax));
         WeatherIcon = weatherIcon;
      }

      private double ConvertKelvinToC(double k)
      {
         return k - 273.15;
      }

      private double _maximumTemperature = 0.00;
      public double MaximumTemperature
      {
         get { return _maximumTemperature; }
         set
         {
            if (_maximumTemperature != value)
            {
               _maximumTemperature = value;
               OnPropertyChanged();
            }
         }
      }

      private double _minimumTemperature = 0.00;
      public double MinimumTemperature
      {
         get { return _minimumTemperature; }
         set
         {
            if (_minimumTemperature != value)
            {
               _minimumTemperature = value;
               OnPropertyChanged();
            }
         }
      }

      private double _currentTemperature = 0.00;
      public double CurrentTemperature
      {
         get { return _currentTemperature; }
         set
         {
            if (_currentTemperature != value)
            {
               _currentTemperature = value;
               OnPropertyChanged();
            }
         }
      }

      private string _weatherIcon;
      public string WeatherIcon
      {
         get { return _weatherIcon; }
         set
         {
            if (_weatherIcon != value)
            {
               _weatherIcon = value;

               ImageURL = "https://openweathermap.org/img/wn/" + _weatherIcon + "@2x.png";
               OnPropertyChanged();
            }
         }
      }

      private string _imageURL = "https://openweathermap.org/img/wn/01d@2x.png";
      public string ImageURL
      {
         get { return _imageURL; }
         set
         {
            if (_imageURL != value)
            {
               _imageURL = value;
               OnPropertyChanged();
            }
         }
      }
   }
}
