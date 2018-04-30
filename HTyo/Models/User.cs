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
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name="Billing Address")]
        public string BillingAddress { get; set; }
        [Required]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        [Display(Name="House Type")]
        public HouseType? HouseType { get; set; }

        [Display(Name="Floor Area")]
        public string FloorArea { get; set; }

        [Display(Name="Lot Area")]
        public string LotArea { get; set; }

     
        public ICollection<JobOrder> JobOrders { get; set; }
    }
}
