using System;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;

namespace MeteoAppSkeleton.ViewModels
{
   internal class LocationsListViewModel : BaseViewModel
   {
      private Models.Location currentLocation;
      private ObservableCollection<Models.Location> _entries;

      public ObservableCollection<Models.Location> Entries
      {
         get { return _entries; }
         set
         {
            _entries = value;
            OnPropertyChanged();
         }
      }

      public LocationsListViewModel()
      {
         _entries = new ObservableCollection<Models.Location>();
         InitLocations();
      }

      private void InitLocations()
      {

         ListenPositionChanges();

         App.Database.GetItemsAsync().Result.ForEach((Models.Location location) =>
         {
            _entries.Add(location);
         });
      }

      private void ListenPositionChanges()
      {
         CrossGeolocator.Current.PositionChanged += async (object sender, PositionEventArgs e) =>
         {
            var placemarks = await Geocoding.GetPlacemarksAsync(e.Position.Latitude, e.Position.Longitude);
            Models.Location currentLocation = new Models.Location(placemarks.First().Locality);
            
            if(this.currentLocation == null)
            {
               _entries.Insert(0, currentLocation);
            } else
            {
               _entries[0] = currentLocation;
            }

            this.currentLocation = currentLocation;
         };
      }

      public bool IsCurrentLocation(Models.Location loc)
      {
         return loc.ID.Equals(currentLocation.ID);
      }

      public void AddLocation(Models.Location loc)
      {
         App.Database.SaveItemAsync(loc);
         _entries.Add(loc);
      }

      public Models.Location GetCurrentLocation()
      {
         return currentLocation;
      }
   }
}
