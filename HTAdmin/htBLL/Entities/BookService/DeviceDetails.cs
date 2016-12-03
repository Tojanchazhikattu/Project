using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace htBLL
{
    public class DeviceDetails
    {
        [Required(ErrorMessage = "Please select Device Type")]
        public int DeviceTypeID { get; set; }
        [Required(ErrorMessage = "Please select Product Type")]
        public int ProductTypeId { get; set; }
         [Required(ErrorMessage = "Please select Handset Model")]
        public int HandsetModelID { get; set; }
        [Required(ErrorMessage = "Please select Device Colour")]
        public int  DeviceColourID { get; set; }
        [Required(ErrorMessage = "Please enter IMEINo")]
        [StringLength(30, MinimumLength = 14, ErrorMessage = "IMEI no should be atleast 14 digits")]
        [Display(Name = "IMEINo")]
        public string  IMEINo { get; set; }
        [Required(ErrorMessage = "Please enter PassCode")]
        public string PassCode { get; set; }
        [Required(ErrorMessage = "Please select Network")]
        public int NetworkID { get; set; }

        //For Computer
        [Display(Name = "Serial No")]
        [Required(ErrorMessage = "Please Serial No")]
        public string SerialNo { get; set; }
        [Display(Name = "Ant-virus Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:s}")]
        public DateTime AntivirusExpiryDate { get; set; }
    }
}

