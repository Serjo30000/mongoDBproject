using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class UserGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("newLastUser")]
        public string NewLastUser { get; set; }
        [BsonElement("fieldUser")]
        public List<User>? FieldUser { get; set; } = new List<User>();
    }
}
