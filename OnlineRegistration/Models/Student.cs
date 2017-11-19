using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class Student
    {
        public long UserId { get; set; }

        public long EmpOrStdId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }



        public byte[] BioPhoto { get; set; }
        public string PhotoByteString { get; set; }

        public long StudentId { get; set; }

        public string StudentCode { get; set; }

        public string StudentName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }


        public string Hall { get; set; }

        public long DepartmentId { get; set; }

        public string DepartName { get; set; }
        public string Address { get; set; }

        public string ContactNo { get; set; }
        public string AdmissionYear { get; set; }

        public string Email { get; set; }

        public string BloodGroup { get; set; }

        public string Sex { get; set; }

        // Becomes <input type="date" ... >
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}