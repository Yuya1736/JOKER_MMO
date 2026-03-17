using JKFrame;
using System;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    public void Init()
    {
        EventSystem.AddTypeEventListener<MonsterSpawnEvent>(OnMonsterSpawn);
    }

    private void OnMonsterSpawn(MonsterSpawnEvent @event)
    {
        MonsterController monster = @event.mainMonsterController;
        monster.Init();
        MonsterClientController monsterClientController = (MonsterClientController)monster.sideController;
        monsterClientController.Init(monster);
    }
}