using Caliburn.Micro;
using pizzamaker.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Singletons
{
    class DatabaseHelperProxy : SqlHelper
    {
        private DatabaseHelperProxy()
        {

        }
        private static DatabaseHelperProxy databaseHelperProxy;
        public static DatabaseHelperProxy getInstance() {
            if (databaseHelperProxy == null) {
                lock (typeof(DatabaseHelperProxy)) {
                    if (databaseHelperProxy == null) {
                        databaseHelperProxy = new DatabaseHelperProxy();
                    }
                }
            }
            return databaseHelperProxy;
        }
        Dictionary<string, BindableCollection<Food>> cache = new Dictionary<string, BindableCollection<Food>>();
        Dictionary<int, byte[]> picturecache = new Dictionary<int, byte[]>();
        DatabaseHelper databaseHelper = DatabaseHelper.getInstance();
        public override void AddLog(string what, string icommand, string message)
        {
            databaseHelper.AddLog(what, icommand, message);
        }

        public override BindableCollection<Food> GetFoodsByType(string type)
        {
            if (cache.ContainsKey(type)) return cache[type];
            cache.Add(type, databaseHelper.GetFoodsByType(type));
            return cache[type];
        }

        public override byte[] GetRawPicture(int id)
        {
            if (picturecache.ContainsKey(id)) return picturecache[id];
            picturecache.Add(id, databaseHelper.GetRawPicture(id));
            return picturecache[id];
        }
    }
}
