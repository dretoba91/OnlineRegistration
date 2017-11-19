using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class CourseReg
    {
        public long CourseRegId { get; set; }
        public long EmployeeId { get; set; }

        public string StudentCode { get; set; }

        public string From{get;set;}

        public string To{get; set;}

        public long ControlNoticeId { get; set; }


        [DataType(DataType.Date)]
        public DateTime CourseRegDate { get; set; }

        public bool IsDeposite { get; set; }

        public bool Approval { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}