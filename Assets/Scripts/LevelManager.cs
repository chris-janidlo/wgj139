using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

    // TODO: waves n shit
public class LevelManager : Singleton<LevelManager>
{
    public List<Transform> EnemySpawnPoints;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    void Start ()
    {
        FlockFactory.Instance.SpawnPlayer(Vector3.zero);

        foreach (var spawn in EnemySpawnPoints)
        {
            FlockFactory.Instance.SpawnEnemy(spawn.position);
        }
    }
}
