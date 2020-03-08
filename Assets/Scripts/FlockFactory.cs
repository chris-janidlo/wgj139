using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class FlockFactory : Singleton<FlockFactory>
{
    public int PlayerStartingFlockSize;
    public Vector2Int EnemyFlockSizeRange;

    public Player PlayerPrefab;
    public FlockLeader EnemyLeaderPrefab;
    public FlockFollower FollowerPrefab;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    public void SpawnPlayer (Vector3 position)
    {
        spawnFlock(position, PlayerPrefab, PlayerStartingFlockSize);
    }

    public void SpawnEnemy (Vector3 position)
    {
        spawnFlock(position, EnemyLeaderPrefab, RandomExtra.Range(EnemyFlockSizeRange));
    }

    void spawnFlock (Vector3 position, FlockLeader leaderPrefab, int flockSize)
    {
        var leader = Instantiate(leaderPrefab, position, Quaternion.identity);

        for (int i = 0; i < flockSize; i++)
        {
            var follower = Instantiate
            (
                FollowerPrefab,
                position + Random.insideUnitSphere * leader.InitialMaxRadius,
                Quaternion.identity
            );

            follower.SetLeader(leader);
        }
    }
}
