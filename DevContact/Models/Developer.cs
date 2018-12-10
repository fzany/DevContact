using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace DevContact.Models
{
    /// <summary>
    /// Developer class for representing the contact information as objects.
    /// </summary>
    public class Developer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Guid { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Phone_Number { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GitHub_Url { get; set; } = string.Empty;
        public string Stackoverflow_Url { get; set; } = string.Empty;
        public Stack Stack { get; set; }
        public Platform Platform { get; set; }
        public string LinkedIn_Url { get; set; } = string.Empty;
        public int Years_Of_Experience { get; set; }
        
    }
}
