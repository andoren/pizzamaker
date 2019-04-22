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
        private  readonly object lockobjc = new object(); 
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
        // Its a cache for the foods like doughs, sauces, etc...
        volatile Dictionary<string, BindableCollection<Food>> cache = new Dictionary<string, BindableCollection<Food>>();
        // Its a cache for the foods picture.
        volatile Dictionary<int, byte[]> picturecache = new Dictionary<int, byte[]>();
        DatabaseHelper databaseHelper = DatabaseHelper.getInstance();
        /// <summary>
        /// Log into the database. First param: Which class caused the error. Second param: Which Command caused the error. Third param: The message that exception has created.
        /// </summary>
        /// <param name="what"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public override void AddLog(string what, string icommand, string message)
        {
            databaseHelper.AddLog(what, icommand, message);
        }

        public override BindableCollection<Food> GetFoodsByType(string type)
        {
            lock (lockobjc)
            {
                if (cache.ContainsKey(type)) return cache[type];
                cache.Add(type, databaseHelper.GetFoodsByType(type));
                return cache[type];
            }
        }

        public override byte[] GetRawPicture(int id)
        {
            lock (lockobjc) {            
            if (picturecache.ContainsKey(id)) return picturecache[id];
            picturecache.Add(id, databaseHelper.GetRawPicture(id));
            return picturecache[id];
            }
        }
    }
}
