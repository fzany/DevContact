using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevContact.Models
{
    public class DeveloperResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Developer Data { get; set; }
    }
    public class DeveloperResponses
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
        public List<Developer> Data { get; set; }
    }
}
