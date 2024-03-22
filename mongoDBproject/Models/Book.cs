using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("family")]
        public string Family { get; set; }
        [BsonElement("secondName")]
        public string SecondName { get; set; }
        [BsonElement("nameBook")]
        public string NameBook { get; set; }
        [BsonElement("publish")]
        public string Publish { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("price")]
        public int Price { get; set; }
        [BsonElement("count")]
        public int Count { get; set; }
    }
}
