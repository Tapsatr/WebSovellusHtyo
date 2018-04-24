using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public string HouseType { get; set; }

        public string FloorArea { get; set; }

        public string LotArea { get; set; }

     
        public ICollection<JobOrder> JobOrders { get; set; }
    }
}
