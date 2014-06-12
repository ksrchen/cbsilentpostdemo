using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pts.domain;
using pts.models;

namespace cbsilentpostdemo
{
    public partial class receipt : System.Web.UI.Page
    {
        IProfileDomain _profileDomain = new ProfileDomain();
        public CreateTokenResponse Response = new CreateTokenResponse();

        protected void Page_Load(object sender, EventArgs e)
        {

            int profileId = 0;
            if (Request.QueryString.AllKeys.Contains("profile"))
            {
                int.TryParse(Request.QueryString["profile"], out profileId);
            }

            var profile = _profileDomain.Get(profileId);

            var parameters = new Dictionary<string, string>();
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

            Response = processResponse(profile, parameters);
        }

        private CreateTokenResponse processResponse(Profile profile, Dictionary<string, string> data)
        {
            var response = new CreateTokenResponse();
            if (data["decision"] == "ACCEPT")
            {
                response.Status = true;
                response.Token = string.Format("p{0}-{1}", profile.ProfileID,  data["payment_token"]);
                response.ReasonCode = data["reason_code"];
                response.CreditCard = new CreditCard();
                response.CreditCard.CardNumber = data["req_card_number"];
                response.CreditCard.CardType = data["req_card_type"];

                var parts = data["req_card_expiry_date"].Split(new char[] { '-' });
                if (parts.Count() > 1)
                {
                    response.CreditCard.ExpirationMonth = parts[0];
                    response.CreditCard.ExpirationYear = parts[1];
                }

            }
            else
            {
                response.Status = false;
                response.Message = data["message"];
                response.ReasonCode = data["reason_code"];
            }
            return response;
        }
    }
}