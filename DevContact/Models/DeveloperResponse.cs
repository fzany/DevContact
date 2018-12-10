using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevContact.Models
{
    /// <summary>
    /// Class for sending a single contact to the request.
    /// </summary>
    public class DeveloperResponse : GeneralResponse
    {
        public Developer Data { get; set; }
    }
    /// <summary>
    /// Class for sending a collection of contacts to the request.
    /// </summary>
    public class DeveloperResponses : GeneralResponse
    {
        public List<Developer> Data { get; set; }
    }

    public class GeneralResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
