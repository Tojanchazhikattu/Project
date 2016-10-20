using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace htBLL
{
    public class DeviceDetails
    {
        [Required]
        public int DeviceTypeID { get; set; }
         [Required]
        public int HandsetModelID { get; set; }
        [Required]
        public int  DeviceColourID { get; set; }
        [Required ]
        [Display(Name = "IMEINo")]
        public string  IMEINo { get; set; }
        [Required]
        public string PassCode { get; set; }
        [Required]
        public int NetworkID { get; set; }          
    }
}