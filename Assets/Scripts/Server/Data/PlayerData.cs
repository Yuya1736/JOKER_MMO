using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

public class PlayerData
{
    [BsonId]
    public string name;
    public string password;
    public CharacterData characterData = new CharacterData() {position = ServerResSystem.serverConfig.defaultPlayerBirthPos};
    public BagData bagData = new BagData();
    public TaskDatas taskDatas = new TaskDatas();
    public string weaponName = "Weapon_0"; // Ĭ�ϳ�ʼΪWeapon_0
    public float hp = 100f;
}

public class CharacterData
{
    public Vector3 position;
    public float rotate_Y;
}