using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace htBLL
{
    public class EquipmentInformation
    {
        [Required]
        public bool Handset { get; set; }
        [Required]
        public bool Simcard { get; set; }
        [Required]
        public bool MemoryCard { get; set; }
        [Required]
        public bool Battery { get; set; }
        [Required]
        public bool BackCover { get; set; }
        public string OtherAccessories { get; set; }       
    }
}