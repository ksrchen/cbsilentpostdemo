using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace mpps.Controllers
{
    public abstract class MppsBaseController : ApiController
    {
        public models.Profile Profile { get; set; }
    }
}
