using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class BookGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("avgPrice")]
        public double AvgPrice { get; set; }
        [BsonElement("firstPublic")]
        public string FirstPublic { get; set; }
        [BsonElement("lastPublic")]
        public string LastPublic { get; set; }
        [BsonElement("fieldBook")]
        public List<Book>? FieldBook { get; set; } = new List<Book>();
    }
}
