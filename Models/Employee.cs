using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunExample.Models
{
    public class Employee
    {
        [DisplayName("Serial No.")]
        public int Serno { get; set; }

        [DisplayName("Employee ID")]
        public string EmpID { get; set; }

        [Required]
        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        public string Dob { get; set; }

        [Required]
        [DisplayName("Passport Number")]
        public string PassNo { get; set; }

        [Required]
        [DisplayName("Nationality")]
        public string Nationality { get; set; }

        [Required]
        [DisplayName("Place Of Birth")]
        public string Pob { get; set; }

        [Required]
        [DisplayName("Visa ID")]
        public string VisaId { get; set; }

        [Required]
        [DisplayName("Visa Type")]
        public string VisaType { get; set; }

        [Required]
        [DisplayName("Branch")]
        public string Branch { get; set; }

        [Required]
        [DisplayName("Basic Salary")]
        public decimal BasicSalary  { get; set; }

        [Required]
        [DisplayName("Allowance")]
        public decimal Allowance { get; set; }

        [Required]
        [DisplayName("Salry Deductions")]
        public decimal Salarydeduct { get; set; }

        [Required]
        [DisplayName("Net Salry")]
        public decimal NetSalary { get; set; }

        
        [DisplayName("Status")]
        public string Status { get; set; }

    }
}