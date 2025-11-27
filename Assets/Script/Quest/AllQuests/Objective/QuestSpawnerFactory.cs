using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestSpawnerFactory
{
    private Dictionary<Type, IObjectiveSpawner> spawners = new Dictionary<Type, IObjectiveSpawner>();

    public void RegisterSpawner<T>(IObjectiveSpawner spawner) where T : Quest
    {
        spawners[typeof(T)] = spawner;
    }

    public IObjectiveSpawner GetSpawner(Quest quest)
    {
        Type type = quest.GetType();
        if (spawners.TryGetValue(type, out IObjectiveSpawner spawner))
            return spawner;

        throw new Exception("No spawner registered for quest: " + quest.questName);
    }
}
