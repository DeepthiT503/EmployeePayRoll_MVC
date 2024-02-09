using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class EmployeeModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+(?: [a-zA-Z]+)*$")]
        public string FullName { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public string Department { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Salary is not valid")]
        public decimal Salary { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string Notes { get; set; }
    }
}
