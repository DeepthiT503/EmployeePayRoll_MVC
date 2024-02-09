using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        public int EmployeeId { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+(?: [a-zA-Z]+)*$")]
        public string FullName { get; set; }
    }
}
