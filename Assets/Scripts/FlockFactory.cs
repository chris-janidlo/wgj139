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

    int index;

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
        var container = new GameObject("flock container " + index++).transform;

        leader.transform.parent = container;

        for (int i = 0; i < flockSize; i++)
        {
            var follower = Instantiate
            (
                FollowerPrefab,
                position + Random.insideUnitSphere * leader.InitialMaxRadius,
                Quaternion.identity,
                container
            );

            follower.SetLeader(leader);
        }
    }
}
