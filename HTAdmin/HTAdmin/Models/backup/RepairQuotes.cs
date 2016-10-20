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
        public Repair objRepair { get; set; }
        public UnLocking objUnLocking { get; set; }
        //public RepairType myRepairType { get; set; }
        public int RepairTypeId { get; set; }
        public IEnumerable<SelectListItem> selectList;
        
        public IList<RepairType> lstRepairType;

        public RepairQuotes()
        {
            lstRepairType = RepairType.FetchAll();
            //objRepair = new Repair();
            //objUnLocking = new UnLocking();
        }
        public void initializeRepair(int deviceTypeID)
        {
            objRepair = new Repair(deviceTypeID);
            selectList = from s in objRepair.lstSmartDeviceRepair
                         select new SelectListItem
                         {
                             Value = s.id.ToString(),
                             Text = s.fault + "   £" + s.price.ToString()
                         };
        }
        public void initializeUnLocking()
        {
            objUnLocking = new UnLocking();
        }
    }


}