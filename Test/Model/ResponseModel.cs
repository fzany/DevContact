using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Test.Model
{
   public class ResponseModel
    {
        public string Data { get; set; }
        public HttpResponseMessage Response { get; set; }
    }
}
