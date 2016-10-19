using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using htBLL;

namespace HTAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View("Index", new HomeModel());
        }


        [HttpPost]
        public JsonResult GetNewRequestList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                HomeService homeService = new HomeService();

                var newRequests = homeService.GetNewRequests(RequestStatus.New);
                var newRequestsCount = newRequests.Count;
                string[] sortParms = jtSorting.Split(' ');
                //var newRequestsSorted = homeService.SortObjectList(newRequests, sortParms[0], sortParms[1]);


                return Json(new { Result = "OK", Records = newRequests, TotalRecordCount = newRequestsCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [HttpPost]
        public JsonResult UpdateNewRequestList(ServiceRequestsComposite serviceRequestsComposite)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                HomeService homeService = new HomeService();
                homeService.UpdateServiceRequest(serviceRequestsComposite);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult GetRequestStatus()
        {
            try
            {
                HomeService homeService = new HomeService();
                var requestStatus = homeService.GetRequestStatus().Select(c => new { DisplayText = c.Description, Value = c.RequestStatusID }); ;
                return Json(new { Result = "OK", Options = requestStatus });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Hello contact";

        //    return View();
        //}

    }
}
