using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTAdmin.Models;
using htBLL;

namespace HTAdmin.Controllers.Book
{
    public class BookSmartDeviceController : Controller
    {
        //
        // GET: /BookSmartDevice/
       // public IEnumerable<SelectListItem> selectList;

        public ActionResult DeviceDetails()
        {
            return View("DeviceDetails", GetDeviceDetails());
        }
        public ActionResult EquipmentInformation()
        {
            return View("EquipmentInformation", GetEquipmentInformation());
        }
        public ActionResult CustomerInformation()
        {
            return View("CustomerInformation", GetCustomerInformation());
        }
         public ActionResult RepairQuotes()
         {
             return View("RepairQuotes", GetRepairQuotes());
         }

         public ActionResult RequestSaved()
         {
             return View();
         }
        private DeviceDetails GetDeviceDetails()
        {
            if (Session["DeviceDetails"] == null)
            {
                Session["DeviceDetails"] = new DeviceDetails();
            }
            return (DeviceDetails)Session["DeviceDetails"];
        }

        private EquipmentInformation GetEquipmentInformation()
        {
            if (Session["EquipmentInformation"] == null)
            {
                Session["EquipmentInformation"] = new EquipmentInformation();
            }
            return (EquipmentInformation)Session["EquipmentInformation"];
        }

        private CustomerInformation GetCustomerInformation()
        {
            if (Session["CustomerInformation"] == null)
            {
                Session["CustomerInformation"] = new CustomerInformation();
            }
            return (CustomerInformation)Session["CustomerInformation"];
        }
        private RepairQuotes GetRepairQuotes()
        {
            if (Session["RepairQuotes"] == null)
            {
                RepairQuotes objRepairQuotes= new RepairQuotes();
                Session["RepairQuotes"] = objRepairQuotes;
            }
            return (RepairQuotes)Session["RepairQuotes"];
        }
        


        private void RemoveCustomer()
        {
            Session.Remove("DeviceDetails");
        }

        public JsonResult GetDeviceTypes()
        {
            int selectedDeviceTypeId=0;
            DeviceDetails obj = GetDeviceDetails();
            if (obj != null)
            {
                if (obj.DeviceTypeID != 0)
                {
                    selectedDeviceTypeId = obj.DeviceTypeID;//(obj.DeviceTypeID == null) ? 0 : (Int32)obj.DeviceTypeID;
                }
            }

            BookSmartDeviceService bookSmartDeviceService = new BookSmartDeviceService();
            var resultData = bookSmartDeviceService.GetAllDeviceType()
                .Select(c => new { Value = c.DeviceTypeId, Text = c.Name, Selected = (c.DeviceTypeId == selectedDeviceTypeId) ? 1 : 0 }).ToList();


            
            return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
            //list.Where(w => w.Name == "height").ToList().ForEach(s => s.Value = 30);
        }

        public JsonResult GetHandSetModel(int deviceTypeId)
        {
            int selectedHandsetModelID = 0;
            DeviceDetails obj = GetDeviceDetails();
            if (obj != null)
            {
                if (obj.HandsetModelID != 0)
                {
                    selectedHandsetModelID = obj.HandsetModelID; //(obj.HandsetModelID == null) ? 0 : (Int32)obj.HandsetModelID;
                }
            }

            BookSmartDeviceService bookSmartDeviceService = new BookSmartDeviceService();
            var resultHandSetModels = bookSmartDeviceService.GetHandSetModelForDeviceType(deviceTypeId)
                .Select(c => new { Value = c.HandsetModelId, Text = c.Name, Selected = (c.HandsetModelId == selectedHandsetModelID) ? 1 : 0 })
                .ToList();
            return Json(new { result = resultHandSetModels }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHandSetModelSelected()
        {
            int selectedHandsetModelID = 0;

            DeviceDetails obj = GetDeviceDetails();
            if (obj != null)
            {
                if (obj.HandsetModelID != 0)
                {
                    selectedHandsetModelID = obj.HandsetModelID;//(obj.HandsetModelID == null) ? 0 : (Int32)obj.HandsetModelID;  
                }
            }
           
            BookSmartDeviceService bookSmartDeviceService = new BookSmartDeviceService();
            DeviceDetails objDeviceType = GetDeviceDetails();
            
            var resultHandSetModels = bookSmartDeviceService.GetHandSetModelForDeviceType(obj.DeviceTypeID)
                .Select(c => new { Value = c.HandsetModelId, Text = c.Name, Selected = (c.HandsetModelId == selectedHandsetModelID) ? 1 : 0 })
                .ToList();
            return Json(new { result = resultHandSetModels }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetColours()
        {
            int selectedDeviceColourID = 0;
            DeviceDetails obj = GetDeviceDetails();
            if (obj != null)
            {
                if (obj.DeviceColourID != 0)
                {
                    selectedDeviceColourID = obj.DeviceColourID;
                }
            }

            BookSmartDeviceService bookSmartDeviceService = new BookSmartDeviceService();
            var resultColours = bookSmartDeviceService.GetColours()
                .Select(c => new { Value = c.ColourId, Text = c.colour, Selected = (c.ColourId == selectedDeviceColourID) ? 1 : 0 }).ToList();
            return Json(new { result = resultColours }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNetworks()
        {
            int selectedNetworkID = 0;
            DeviceDetails obj = GetDeviceDetails();
            if (obj != null)
            {
                if (obj.NetworkID != 0)
                {
                    selectedNetworkID = obj.NetworkID;
                }
            }

            BookSmartDeviceService bookSmartDeviceService = new BookSmartDeviceService();
            var resultColours = bookSmartDeviceService.GetNetworks()
                .Select(c => new { Value = c.NetworkId, Text = c.name, Selected = (c.NetworkId == selectedNetworkID) ? 1 : 0 }).ToList();
            return Json(new { result = resultColours }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SubmitDeviceDetails(string submit, FormCollection fc)
        {

            if (submit == "Next")
            {
                if (ModelState.IsValid)
                {
                    DeviceDetails obj = GetDeviceDetails();
                    obj.DeviceTypeID = int.Parse(fc["DeviceTypeID"]);
                    obj.HandsetModelID = int.Parse(fc["HandsetModelID"]);
                    obj.DeviceColourID = int.Parse(fc["DeviceColourID"]);
                    obj.IMEINo = fc["IMEINo"].ToString();
                    obj.PassCode = fc["PassCode"].ToString();
                    int selectedNetworkID = 0;
                    obj.NetworkID = (int.TryParse(fc["ddlNetwork"], out selectedNetworkID))?selectedNetworkID:0;

                    return RedirectToAction("EquipmentInformation");
                    //SubmitDeviceDetails
                    //return View("EquipmentInformation");
                }
            }
            return View();
        }
        public ActionResult SubmitEquipmentInformation(string submit, EquipmentInformation equipInfo)
        {
            
                if (ModelState.IsValid)
                {
                    EquipmentInformation obj = GetEquipmentInformation();
                    obj.Handset = equipInfo.Handset;
                    obj.Simcard = equipInfo.Simcard;
                    obj.MemoryCard = equipInfo.MemoryCard;
                    obj.Battery = equipInfo.Battery;
                    obj.BackCover = equipInfo.BackCover;
                    obj.OtherAccessories = equipInfo.OtherAccessories;
                    if (submit == "Next")
                    {
                        return RedirectToAction("CustomerInformation");
                    }
                    else if (submit == "Prev")
                    {
                        return RedirectToAction("DeviceDetails");
                    }
                }
            
            return View();
        }
        public ActionResult SubmitCustomerInformation(string submit, CustomerInformation custInfo)
        {
           if (ModelState.IsValid)
                {
                    CustomerInformation obj = GetCustomerInformation();
                    obj.SearchChannelId = custInfo.SearchChannelId;
                    obj.FirstName = custInfo.FirstName;
                    obj.LastName = custInfo.LastName;
                    obj.ContactNo = custInfo.ContactNo;
                    obj.Email = custInfo.Email;
                    obj.Address = custInfo.Address;
                    obj.PostCode = custInfo.PostCode;
                    if (submit == "Next")
                    {
                        return RedirectToAction("RepairQuotes");
                    }
                    else if (submit == "Prev")
                    {
                        return RedirectToAction("EquipmentInformation");
                    }
                }
            
            return View();
        }
        
        [HttpGet]
    public ActionResult GetRepairTypeView(int selectedValue) // could use an enum for the selectable values
    {
        var model = GetRepairQuotes();
        string partialViewName = string.Empty;
        RepairType  selectedRepairType= model.lstRepairType.Where(s => s.RepairTypeId == selectedValue).FirstOrDefault();
        if (selectedRepairType!= null)
        {
            switch (selectedRepairType.Name)
            {
                case "Repair":
                    DeviceDetails deviceDetails = GetDeviceDetails();                    
                    model.initializeRepair(deviceDetails.DeviceTypeID);
                    partialViewName = "RepairView";
                    break;
                case "UnLocking":
                    partialViewName = "UnLockingView";
                    break;

                default:
                    throw new ArgumentException("unknown selected value", "selectedValue");
                    break;
            }
        }

        return PartialView(partialViewName, model);
    }


    [HttpPost, ActionName("SubmitRepairQuotes")]
    public ActionResult SubmitRepairQuotes(string submit, RepairQuotes repairQuotes)
    {
       
                if (ModelState.IsValid)
                {
                    RepairQuotes objRepairQuotes = GetRepairQuotes();
                    objRepairQuotes.RepairTypeId = repairQuotes.RepairTypeId;
                    objRepairQuotes.serviceDetails.OtherRepair = repairQuotes.serviceDetails.OtherRepair;
                    objRepairQuotes.serviceDetails.OtherRepairCost = repairQuotes.serviceDetails.OtherRepairCost;
                    objRepairQuotes.serviceDetails.CollectionDateTime = repairQuotes.serviceDetails.CollectionDateTime;
                    objRepairQuotes.serviceDetails.MessageToEngineer = repairQuotes.serviceDetails.MessageToEngineer;
                    objRepairQuotes.serviceDetails.EngineerUserId = repairQuotes.serviceDetails.EngineerUserId;
                    objRepairQuotes.lstSelectedRepairs = repairQuotes.lstSelectedRepairs;

                    objRepairQuotes.serviceDetails.UnlockingCost = repairQuotes.serviceDetails.UnlockingCost;
                    objRepairQuotes.serviceDetails.AdancePayment = repairQuotes.serviceDetails.AdancePayment;
                    objRepairQuotes.serviceDetails.AdancePaymentAmount = repairQuotes.serviceDetails.AdancePaymentAmount;

                    if (submit == "Finish")
                    {
                        BookSmartDeviceService bookSmartDeviceService = new BookSmartDeviceService();
                        bookSmartDeviceService.SaveSmartDeviceRequest(objRepairQuotes.serviceDetails,
                                                                        GetCustomerInformation(),
                                                                        GetDeviceDetails(),
                                                                        GetEquipmentInformation(),
                                                                        repairQuotes.RepairTypeId,
                                                                        objRepairQuotes.lstSelectedRepairs);
                        return RedirectToAction("RequestSaved");
                    }
                    else if (submit == "Previous")
                    {
                        return RedirectToAction("CustomerInformation");
                    }
                    
                }
            
        return View();

    }
        
    }
}
