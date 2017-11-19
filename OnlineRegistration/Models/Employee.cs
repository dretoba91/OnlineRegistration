using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class Employee
    {
        public long UserId { get; set; }

        public long EmpOrStdId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public long EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public long DepartmentId { get; set; }

        public string DepartmentName { get; set; }


        public string Address { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string BloodGroup { get; set; }

        public string Sex { get; set; }

        // Becomes <input type="date" ... >
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        public byte[] BioPhoto { get; set; }

        public string PhotoByteString { get; set; }
        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}