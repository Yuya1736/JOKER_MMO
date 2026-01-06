using JKFrame;
using MongoDB.Driver;
using UnityEngine;

public class DataBaseManager : SingletonMono<DataBaseManager>
{
    [SerializeField] private string connectStr = "mongodb://localhost:27017/";
    public MongoClient mongoClient;
    public IMongoDatabase mongoDatabase;
    public IMongoCollection<PlayerData> playerDataCollection;
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        mongoClient = new MongoClient(connectStr);
        mongoDatabase = mongoClient.GetDatabase("MMO");
        playerDataCollection = mongoDatabase.GetCollection<PlayerData>(nameof(PlayerData));
    }

    public PlayerData GetPlayerData(string name)
    {
        return playerDataCollection.Find(Builders<PlayerData>.Filter.Eq(nameof(PlayerData.name), name)).FirstOrDefault();
    }

    public void AddPlayerData(PlayerData playerData)
    {
        playerDataCollection.InsertOne(playerData);
    }

    public void SavePlayerData(PlayerData playerData)
    {
        playerDataCollection.ReplaceOne(Builders<PlayerData>.Filter.Eq(nameof(PlayerData.name), name), playerData);
    }
}
