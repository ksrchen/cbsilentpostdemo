using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pts.domain
{
    public class KeyStore
    {
        public static pts.models.Key Get(Guid guid)
        {
            using (var db = new pts.models.storeContainer())
            {
                return db.Keys.FirstOrDefault(p => p.Id == guid);
            }
        }

        public static void Insert(pts.models.Key key)
        {
            using (var db = new pts.models.storeContainer())
            {
                db.Keys.Add(key);
                db.SaveChanges();
            }
        }
    }
}
