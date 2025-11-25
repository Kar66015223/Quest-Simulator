using System.Collections.Generic;
using UnityEngine;

public class QuestSpawnerFactory
{
    private Dictionary<Quest, IObjectiveSpawner> spawners;

    public IObjectiveSpawner GetSpawner(Quest quest)
    {
        return spawners[quest];
    }
}
