using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_idUser")]
        public string IdUser { get; set; }
        [BsonElement("dateOrder")]
        public DateTime DateOrder { get; set; }
        [BsonElement("countOrder")]
        public int CountOrder { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_idOrderBook")]
        public string IdOrderBook { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_idBook")]
        public string IdBook { get; set; }
    }
}
