﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Models
{
    public class Level
    {
        public long LevelId{get;set;}

        public string LevelCode{get;set;}

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}