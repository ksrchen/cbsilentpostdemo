using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cbsilentpostdemo
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public String getUUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public String getUTCDateTime()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            return time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }
    }
}