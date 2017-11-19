using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class ControlNotice
    {
        public long ControlNoticeId { get; set; }
        public long LevelId { get; set; }
        public long TermId { get; set; }
        public long DepartmentId { get; set; }

        // Becomes <input type="date" ... >
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        //[DataType(DataType.Time)]
       // public DateTime StartTime { get; set; }


        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

       // [DataType(DataType.Time)]
        //public DateTime EndTime { get; set; }
        public bool OnOff { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        ////
        public string LevelCode { get; set; }
        public string TermCode { get; set; }
        public string DepartmentName { get; set; }

        public string Year { get; set; }
    }
}