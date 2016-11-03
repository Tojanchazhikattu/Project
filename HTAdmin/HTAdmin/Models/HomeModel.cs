using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using htBLL;
using System.Web.Mvc;

namespace HTAdmin
{
    public class HomeModel
    {
        public IList<AssignedJobs> lstAssignedJobs { get; set; }
        public IList<FieldModel> lstOrdersByStatus { get; set; }
        public HomeModel()
        {
            HomeService homeService= new HomeService();
            lstAssignedJobs = homeService.GetAssignedJobs();
            lstOrdersByStatus = homeService.GetOrdersByStatus();
        }
    }

    //public class FieldModel
    //{
    //    [Display(Name = "Property")]
    //    public string PropertyName { get; set; }

    //    [Display(Name = "Value")]
    //    public string PropertyValue { get; set; }
    //}
}