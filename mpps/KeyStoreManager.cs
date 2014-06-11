using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pts
{
    public class KeyStoreManager
    {
        public static pts.models.Key createNewKey()
        {
            pts.models.Key key = new models.Key
            {
                Id = Guid.NewGuid(),
                Value = pts.domain.SecurityDomain.sign(DateTime.Now.Ticks.ToString(), Guid.NewGuid().ToString()),
                Date = DateTime.Now
            };

            pts.domain.KeyStore.Insert(key);
            HttpContext.Current.Cache[key.Id.ToString()] = key;
            return key;
        }

        public static pts.models.Key getKey(string id)
        {
            var key = HttpContext.Current.Cache.Get(id) as pts.models.Key;
            if (key != null)
            {
                return key;
            }
            key =  pts.domain.KeyStore.Get(new Guid(id));
            if (key != null)
            HttpContext.Current.Cache[key.Id.ToString()] = key;
            {
                HttpContext.Current.Cache[key.Id.ToString()] = key;
            }
            return key;
        }
    }
}