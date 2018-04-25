using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Models
{
    public enum HouseType
    {
        House, Farm, Summerhouse, Rowhouse
    }
    public class User : IdentityUser
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
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

        public HouseType? HouseType { get; set; }

        public string FloorArea { get; set; }

        public string LotArea { get; set; }

     
        public ICollection<JobOrder> JobOrders { get; set; }
    }
}
