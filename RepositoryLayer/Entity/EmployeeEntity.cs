
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class EmployeeEntity
    {
        public int EmployeeId { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? ImagePath { get; set; }
        public string? Gender { get; set; }
        [Required]
        public string? Department { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string? Notes { get; set; }
    }

}
