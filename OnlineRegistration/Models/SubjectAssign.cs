using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class SubjectAssign
    {
        public long SubjectAssignId { get; set; }

        public long DepartmentId { get; set; }

        public long LevelId { get; set; }

        public long TermId { get; set; }

        public long SubjectId { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}