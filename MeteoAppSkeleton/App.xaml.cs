using MeteoAppSkeleton.Views;
using MeteoAppSkeleton.Database;
using MeteoAppSkeleton.Geolocation;
using MeteoAppSkeleton.Notification;
using MeteoAppSkeleton.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Matcha.BackgroundService;

namespace MeteoAppSkeleton
{
   public partial class App : Application
   {

      private static LocationsDB database;
      private LocationsListViewModel locationList = new LocationsListViewModel();
      private LocationDetailViewModel locationDetail;
      private PeriodicTask periodicTask = new PeriodicTask(30);

      public static LocationsDB Database
      {
         get
         {
            if (database == null)
            {
               database = new LocationsDB();
            }
            return database;
         }
      }

      public App()
      {
         InitializeComponent();
         GeolocationController.StartListener();

         var nav = new NavigationPage(new LocationsListPage())
         {
            BarBackgroundColor = Color.LightGreen,
            BarTextColor = Color.White
         };

         MainPage = nav;
      }

      protected override void OnStart()
      {
         BackgroundAggregatorService.Add(() => periodicTask);
         BackgroundAggregatorService.StartBackgroundService();
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
         Models.Location currentLocation = locationList.GetCurrentLocation();
         locationDetail = new LocationDetailViewModel(currentLocation);
         periodicTask.Title = currentLocation.Name;
         periodicTask.Content = "Set as current location";
      }
   }
}
