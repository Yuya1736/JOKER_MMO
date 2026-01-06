using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

public class PlayerData
{
    [BsonId]
    public string name;
    public string password;
    public CharacterData characterData = new CharacterData() {position = ServerResSystem.serverConfig.defaultPlayerBirthPos};
}

public class CharacterData
{
    public Vector3 position;
    public float rotate_Y;
}