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
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.  .";

            return View("Index", new HomeModel());
        }
        
        
        [HttpGet]
        public ActionResult GetRequestStatusView(HomeModel model) // could use an enum for the selectable values
        {
            return PartialView("RequestStatusView", model);
            //return Json(new { Result = "OK", Message = model.GetOrderNumberByStatus(1) });
        }
        [HttpGet]
        public ActionResult GetRequestStatusViewRefresh() // could use an enum for the selectable values
        {
            HomeModel model = new HomeModel();
            return PartialView("RequestStatusView", model);
            //return Json(new { Result = "OK", Message = model.GetOrderNumberByStatus(1) });
        }

        [HttpPost]
        public JsonResult GetRequestListByStatus(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null, string status=null)
        {
            try
            {
                int requestStatus = 0;
                if (int.TryParse(status, out requestStatus))
                {
                    RequestStatus reqstat = (RequestStatus)requestStatus;
                    HomeService homeService = new HomeService();
                    var requestsWithSelectedStatus = homeService.GetRequestsByStatus(reqstat);
                    var requestsCount = requestsWithSelectedStatus.Count;
                    string[] sortParms = jtSorting.Split(' ');
                    return Json(new { Result = "OK", Records = requestsWithSelectedStatus, TotalRecordCount = requestsCount });
                }
                else
                    return Json(new { Result = "ERROR", Message = "Invalid Status" });

               
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
       


        [HttpPost]
        public JsonResult UpdateRequestList(ServiceRequestsComposite serviceRequestsComposite)
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
        [HttpPost]
        public JsonResult GetEngineers()
        {
            try
            {
                HomeService homeService = new HomeService();
                var requestStatus = homeService.GetEngineers().Select(c => new { DisplayText = c.UserName, Value = c.UserId }); ;
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
