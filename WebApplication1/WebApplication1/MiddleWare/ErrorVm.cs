﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.MiddleWare
{
    public class ErrorVm
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
