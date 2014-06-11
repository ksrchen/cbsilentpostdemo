using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pts.models;
namespace pts.domain
{
    public class ProfileDomain : IProfileDomain
    {
        private List<Profile> LoadProfile()
        {  
            var appDataFolder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var json = File.ReadAllText(Path.Combine(appDataFolder, "profile.json"));
            List<Profile> profiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profile>>(json);
            return profiles;

        }
        public models.Profile Get(int profileId)
        {
            var profiles = LoadProfile();
            return profiles.FirstOrDefault(p => p.ProfileID == profileId);
        }
    }
}
