using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using htBLL;
using System.Web.Mvc;


namespace HTAdmin
{

    public class  RepairQuotes
    {
        //public Repair objRepair { get; set; }

        public ServiceDetails serviceDetails { get; set; }
        public IList<SmartDeviceRepair> lstSmartDeviceRepair { get; set; }
        public IList<User> lstEngineers;
        public List<int> lstSelectedRepairs { get; set; }
        //RepairTypeId will be used in ServiceRequest
        public int RepairTypeId { get; set; }
        public IEnumerable<SelectListItem> slSmartDeviceRepair;
        
        public IList<RepairType> lstRepairType;

        public RepairQuotes()
        {
            lstRepairType = RepairType.FetchAll();
            lstSelectedRepairs = new List<int>();
            lstEngineers = User.FetchAll().Where(s => s.UserTypeId == 2 && s.Status == 1).ToList();
            serviceDetails = new ServiceDetails();
        }
        public void initializeRepair(int? deviceTypeID)
        {
            lstSmartDeviceRepair = SmartDeviceRepair.FetchAll().Where(s => s.DeviceTypeId == deviceTypeID).ToList();
            slSmartDeviceRepair = from s in lstSmartDeviceRepair
                         select new SelectListItem
                         {
                             Value = s.id.ToString(),
                             Text = s.fault + "   £" + s.price.ToString()
                         };
        }

    }


}