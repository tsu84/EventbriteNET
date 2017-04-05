﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventbriteNET.Http
{
    public class QueryParameter
    {
        public string Key { get; set; }
        public string Value { get; set;  }
        public QueryParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
