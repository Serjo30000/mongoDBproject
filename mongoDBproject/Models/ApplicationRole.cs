using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Models
{
    [CollectionName("collectionRoles")]
    public class ApplicationRole : MongoIdentityRole<ObjectId>
    {
    }
}
