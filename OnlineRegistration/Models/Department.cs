using System;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class Department
    {
        public long DepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}