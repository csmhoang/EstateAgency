﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record PostEnums
    {
        public enum IsAcceptPost
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "Accepted")]
            Accepted = 1,
            [EnumMember(Value = "Rejected")]
            Rejected = 2,
        }
        public enum StatusPost
        {
            [EnumMember(Value = "Published")]
            Published = 0,
            [EnumMember(Value = "Deleted")]
            Deleted = 1
        }
    }
}