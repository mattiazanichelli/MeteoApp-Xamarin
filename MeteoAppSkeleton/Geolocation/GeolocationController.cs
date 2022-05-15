using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace MeteoAppSkeleton.Geolocation
{
   class GeolocationController
   {
      public static async Task<Position> GetCurrentPosition()
      {
         var locator = CrossGeolocator.Current; // singleton
         var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
         Debug.WriteLine("Position Status: {0}", position.Timestamp);
         Debug.WriteLine("Position Latitude: {0}", position.Latitude);
         Debug.WriteLine("Position Longitude: {0}", position.Longitude);

         return position;
      }

      public static void StartListener()
      {
         var locator = CrossGeolocator.Current;
         locator.StartListeningAsync(TimeSpan.FromSeconds(5), 50);
      }
   }
}
