using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserName")]
        public string Login { get; set; }
        public string Role { get; set; } = "";
        [BsonElement("PasswordHash")]
        public string Password { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("PhoneNumber")]
        public string NumberPhone { get; set; }
        [BsonElement("NormalizedUserName")]
        public string NormalizedUserName { get; set; }
        [BsonElement("NormalizedEmail")]
        public string NormalizedEmail { get; set; }
        [BsonElement("SecurityStamp")]
        public string SecurityStamp { get; set; }
        [BsonElement("ConcurrencyStamp")]
        public string ConcurrencyStamp { get; set; }
        [BsonElement("EmailConfirmed")]
        public bool EmailConfirmed { get; set; }
        [BsonElement("PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }
        [BsonElement("TwoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }
        [BsonElement("LockoutEnabled")]
        public bool LockoutEnabled { get; set; }
        [BsonElement("LockoutEnd")]
        public string LockoutEnd { get; set; }
        [BsonElement("AccessFailedCount")]
        public int AccessFailedCount { get; set; }
        [BsonElement("Version")]
        public int Version { get; set; }
        [BsonElement("CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [BsonElement("Claims")]
        public List<ObjectId> Claims { get; set; }
        [BsonElement("Logins")]
        public List<ObjectId> Logins { get; set; }
        [BsonElement("Tokens")]
        public List<ObjectId> Tokens { get; set; }
        [BsonElement("Roles")]
        public List<ObjectId> Roles { get; set; }

    }
}
