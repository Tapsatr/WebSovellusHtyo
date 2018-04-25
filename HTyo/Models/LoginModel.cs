
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Models
{
    public class LoginModel
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Please enter password", MinimumLength = 1)]
        public string Password { get; set; }
    }
}
