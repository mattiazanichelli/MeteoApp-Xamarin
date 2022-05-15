using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using MeteoAppSkeleton.Models;

namespace MeteoAppSkeleton.Database
{
   public class LocationsDB
   {
      private readonly SQLiteAsyncConnection database;

      public LocationsDB()
      {
         var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Locations.db");
         database = new SQLiteAsyncConnection(dbPath);

         database.CreateTableAsync<Location>().Wait();
      }

      public Task<List<Location>> GetItemsAsync()
      {
         return database.Table<Location>().ToListAsync();
      }

      public Task<List<Location>> GetItemsWithWhere(string id)
      {
         return database.QueryAsync<Location>("SELECT * FROM [Location] WHERE [Id] =?", id);
      }

      public Task<Location> GetItemAsync(string id)
      {
         return database.Table<Location>().Where(i => i.ID == id).FirstOrDefaultAsync();
      }

      public Task<int> SaveItemAsync(Location loc)
      {
         return database.InsertAsync(loc);
      }

      public Task<int> DeleteItemAsync(Location loc)
      {
         return database.DeleteAsync(loc);
      }
   }
}
