using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class Term
    {
        public long TermId { get; set; }

        public string TermCode { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}