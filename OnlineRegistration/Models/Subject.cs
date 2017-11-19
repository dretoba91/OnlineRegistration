using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class Subject
    {
        public long SubjectId { get; set; }

        public string SubjectCode { get; set; }

        public double Credit { get; set; }

        public string SubjectName { get; set; }

        public string SubjectType { get; set; }

        public long SubjectAssignId { get; set; }
        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}