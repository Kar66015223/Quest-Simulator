using UnityEngine;

public class ObjectiveSpawner
{
    private IObjectiveSpawner _currentSpawner;

    public void SetSpawner(IObjectiveSpawner spawner)
    {
        _currentSpawner = spawner;
    }

    public void Spawn()
    {
        _currentSpawner?.SpawnObjectives();
    }
}
