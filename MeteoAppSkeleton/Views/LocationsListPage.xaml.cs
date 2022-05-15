using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeteoAppSkeleton.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoAppSkeleton.Views
{
   //[XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class LocationsListPage : ContentPage
   {
      private LocationsListViewModel locationsHolder;
      public LocationsListPage()
      {
         InitializeComponent();
         locationsHolder = new LocationsListViewModel();
         BindingContext = locationsHolder;
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
      }

      void OnItemAdded(object sender, EventArgs e)
      {
         PromptAndAddItem();
      }

      async void PromptAndAddItem()
      {
         string result = await DisplayPromptAsync("Insert new Location", "Insert Location Name:");
         locationsHolder.AddLocation(new Models.Location(result));
      }

      void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if(e.SelectedItem is null)
         {
            return;
         }

         Navigation.PushAsync(new LocationDetailsPage()
         {
            BindingContext = new LocationDetailViewModel(e.SelectedItem as Models.Location)
         });
      }
   }
}