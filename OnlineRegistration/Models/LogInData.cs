using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class LogInData
    {
        public int AdminEqual1{get;set;}
        public string UserName { get; set; }

        public long UserId { get; set; }

        public long EmpOrStdId { get; set; }
        public string Password { get; set; }

        public int UserTypeCode { get; set; }

       
                /*common*/

        public byte[] BioPhoto { get; set; }
        public string PhotoByteString { get; set; }
        public string Address { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string BloodGroup { get; set; }

        public string Sex { get; set; }

        public string DepartmentName { get; set; }

                    /*Employee Information*/

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        

        // Becomes <input type="date" ... >
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }


                     /*Student Information*/

        public string StudentCode { get; set; }

        public string StudentName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string Hall { get; set; }

        public string AdmissionYear { get; set; }


        // Becomes <input type="date" ... >
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}