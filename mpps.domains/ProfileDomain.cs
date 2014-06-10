using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace mpts.domain
{
    public class ProfileDomain : IProfileDomain
    {
        public models.Profile Get(int profileId)
        {
            using (var db = new mpts.models.mppsEntities())
            {
                return db.Profiles.Include("Provider").Include("Provider.ProviderSettings").FirstOrDefault(p => p.ProfileID == profileId);
            }
        }
    }
}
