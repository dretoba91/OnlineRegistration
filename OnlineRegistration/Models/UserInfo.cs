using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class UserInfo
    {
        public long UserId { get; set; }

        public long EmpOrStdId { get; set; }
        public string UserName { get; set; }

        public int UserTypeCode { get; set; }

        public string Password { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}