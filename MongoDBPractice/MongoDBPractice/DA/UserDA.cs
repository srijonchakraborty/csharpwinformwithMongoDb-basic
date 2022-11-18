using MongoDB.Driver;
using MongoDBPractice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBPractice.DA
{
    public class UserDA
    {
        IMongoCollection<UserModel> userCollection;
        public UserDA()
        {
            setCollection();
        }
        private void setCollection()
        {
            try
            {
                userCollection = Connections.ConInfo?.MongoDatabase?.GetCollection<UserModel>("users");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public UserModel GetByLoginID(string loginID)
        {
            if (userCollection == null)
                return null;
            var filterDefination = Builders<UserModel>.Filter.Eq(c => c.LoginId, loginID);
            return userCollection.Find(filterDefination).FirstOrDefault();
        }
        public List<UserModel> GetAll()
        {
            if (userCollection == null)
                return new List<UserModel>();
            var filterDefination = Builders<UserModel>.Filter.Empty;
            return userCollection.Find(filterDefination).ToList();
        }
        public void Insert(UserModel obj)
        {
            if (userCollection == null)
                return;
            userCollection.InsertOne(obj);
        }
        public void Update(UserModel obj)
        {
            if (userCollection == null)
                return;
            var filterDefination = Builders<UserModel>.Filter.Eq(c => c.Id, obj.Id);
            var updateDefination = Builders<UserModel>.Update
                .Set(a => a.FirstName, obj.FirstName)
                .Set(a => a.LastName, obj.LastName)
                .Set(a => a.LoginId, obj.LoginId)
                .Set(a => a.Password, obj.Password)
                .Set(a => a.Email, obj.Email);
            userCollection.UpdateOne(filterDefination, updateDefination);
        }
        public void Delete(string id)
        {
            if (userCollection == null)
                return;
            var filterDefination = Builders<UserModel>.Filter.Eq(c => c.Id, id);
            userCollection.DeleteOne(filterDefination);
        }
    }
}
