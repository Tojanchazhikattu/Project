using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace htBLL
{
    public class Repair
    {
        public string OtherRepair { get; set; }
        public decimal OtherRepairCost { get; set; }
        public string MessageToEngineer { get; set; }
        //public IList<SmartDeviceRepair> lstSelectedSmartDeviceRepair { get; set; }
        public int EngineerUserId { get; set; }
        public DateTime CollectionDateTime { get; set; }
        public List<int> lstSelectedRepairs { get; set; }

        public IList<SmartDeviceRepair> lstSmartDeviceRepair { get; set; }


        
        public IList<User> lstEngineers;


        public Repair()
        {

        }
        public Repair(int deviceTypeId)
        {
            lstSmartDeviceRepair = SmartDeviceRepair.FetchAll().Where(s => s.DeviceTypeId == deviceTypeId).ToList();

            lstEngineers = User.FetchAll().Where(s => s.UserTypeId == 2 && s.IsActive == 1).ToList();
            lstSelectedRepairs = new List<int>();
        }

    }
}
