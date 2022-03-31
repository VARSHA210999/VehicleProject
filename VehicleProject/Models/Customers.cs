using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//ADDED
using System.ComponentModel.DataAnnotations;

namespace VehicleProject.Models
{
    public class Customers
    {
        [Key]
        public int CustomerID { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, MaxLength(13)]
        public string Phone { get; set; }
        [Required, MaxLength(30)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(20)]
        public string Password { get; set; }
    }
}