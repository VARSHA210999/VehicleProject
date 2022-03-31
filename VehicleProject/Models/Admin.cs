using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//ADDED
using System.ComponentModel.DataAnnotations;

namespace VehicleProject.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string UserName { get; set; }

        public string Passsword { get; set; }
    }
}