using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class OrderBook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_idOrder")]
        public string IdOrder { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_idBook")]
        public string IdBook { get; set; }
        [BsonElement("mark")]
        public bool Mark { get; set; }
    }
}
