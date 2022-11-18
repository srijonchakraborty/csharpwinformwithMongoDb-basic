using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBPractice.Model
{
    [Serializable]
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("loginId")]
        [BsonRepresentation(BsonType.String)]
        public string? LoginId { get; set; }

        [BsonElement("password")]
        [BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }

        [BsonElement("firstName")]
        [BsonRepresentation(BsonType.String)]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        [BsonRepresentation(BsonType.String)]
        public string? LastName { get; set; }

        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }
    }
}
