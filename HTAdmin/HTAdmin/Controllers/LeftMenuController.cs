using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTAdmin.Controllers
{
    public class LeftMenuController : Controller
    {
        //
        // GET: /LeftMenu/

        public ActionResult Index()
        {
            return PartialView();
        }

    }
}
