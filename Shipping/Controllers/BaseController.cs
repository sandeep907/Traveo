using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Utilities;

namespace Shipping.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public UtilityHelper helper = new UtilityHelper();
    }
}
