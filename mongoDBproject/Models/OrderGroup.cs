using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class OrderGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("avgCountOrder")]
        public double AvgCountOrder { get; set; }
        [BsonElement("lastDateOrder")]
        public DateTime LastDateOrder { get; set; }
        [BsonElement("fieldOrder")]
        public List<Order>? FieldUser { get; set; } = new List<Order>();
    }
}
