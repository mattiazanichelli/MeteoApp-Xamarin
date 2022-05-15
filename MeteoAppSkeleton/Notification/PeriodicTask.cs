using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matcha.BackgroundService;
using Plugin.LocalNotifications;

namespace MeteoAppSkeleton.Notification
{
   public class PeriodicTask : IPeriodicTask
   {
      public TimeSpan Interval { get; set; }

      public string Title { get; set; }

      public string Content { get; set; }

      public PeriodicTask(int seconds)
      {
         this.Title = "MeteoApp";
         this.Content = "Running in background";
         Interval = TimeSpan.FromSeconds(seconds);
      }

      public Task<bool> StartJob()
      {
         CrossLocalNotifications.Current.Show(Title, Content);
         return Task.FromResult(true);
      }
   }
}
