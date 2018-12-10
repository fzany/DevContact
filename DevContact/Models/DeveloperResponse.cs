using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevContact.Models
{
    /// <summary>
    /// Class for sending a single contack to the request.
    /// </summary>
    public class DeveloperResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Developer Data { get; set; }
    }
    /// <summary>
    /// Class for sending a collection of contacts to the request.
    /// </summary>
    public class DeveloperResponses
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
        public List<Developer> Data { get; set; }
    }

    public class GeneralResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
