using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInfo
{
    [BsonId]
    [BsonElement]
    [BsonIgnore]
    public int id;
    public string name;
    public int lv;
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime dateTime;
}

public class ServerLaunch : MonoBehaviour
{
    private void Start()
    {
        //string connetStr = "mongodb://localhost:27017/";
        //MongoClient mongoClient = new MongoClient(connetStr);

        //IMongoDatabase mmoDataBase = mongoClient.GetDatabase("MMO");

        //IMongoCollection<UserInfo> userInfoCollection = mmoDataBase.GetCollection<UserInfo>("UserInfo");

        //UserInfo userInfo = new UserInfo
        //{
        //    id = 1,
        //    name = "Yuyas",
        //    lv = 5
        //};

        //userInfoCollection.InsertOne(userInfo);

        //UserInfo userInfo2 = userInfoCollection.Find(Builders<UserInfo>.Filter.Eq("id", 1)).FirstOrDefault();
        //List<UserInfo> users = userInfoCollection.Find(Builders<UserInfo>.Filter.Gt("id", 1)).ToList();

        //userInfoCollection.UpdateOne(Builders<UserInfo>.Filter.Eq("id", 1), Builders<UserInfo>.Update.Set("lv", 10));
        //userInfoCollection.ReplaceOne(Builders<UserInfo>.Filter.Eq("id", 1), userInfo);

        //userInfoCollection.DeleteOne(Builders<UserInfo>.Filter.Eq("id", 1));


        //return;
        Init();
        SceneManager.LoadScene("GameScene");
    }

    private void Init()
    {
        ServerResSystem.InsatantialteNetworkManager().Init(false);
    }
}
