using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jquerypractice.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department name is required.")]
        public string Department { get; set; }

        [Display(Name = "Job Type")]
        [Required(ErrorMessage = "Job Type must be defined")]
        public string JobType { get; set; }

        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
    }
}