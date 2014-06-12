using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pts.models
{
    public class CreateTokenResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string ReasonCode { get; set; }
        public string  ReferenceNumber { get; set; }
        public string Token { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
