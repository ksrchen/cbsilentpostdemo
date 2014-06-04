using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mpps.domain;

namespace cbsilentpostdemo
{
    public partial class receipt : System.Web.UI.Page
    {
        IProfileDomain _profileDomain = new ProfileDomain();

        public IDictionary<string, string> parameters = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            var profile = _profileDomain.Get(1);

            foreach (var key in Request.Form.AllKeys)
            {
                parameters.Add(key, Request.Params[key]);
            }

            var secret = profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "secret").SettingValue;
            var signature = SecurityDomain.sign(parameters, secret);
            if (signature != parameters["signature"])
            {
                parameters["decision"] = "REJECT";
                parameters["message"] = "signature does not match";
            }
        }
    }
}