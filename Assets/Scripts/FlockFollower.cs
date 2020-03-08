using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockFollower : Bird
{
    public float LeaderChangeTimer { get; private set; }

    public float LeaderChangeCooldown;
    public FlockLeader Leader;

    public LayerMask StealCheckMask;

    [Tooltip("How many birds to count as the cohort of \"local\" flock mates")]
    public int LocalFlockSize;

    new Collider collider => GetComponent<Collider>();

    void Update ()
    {
        LeaderChangeTimer = Mathf.Max(LeaderChangeTimer - Time.deltaTime, 0);

        List<Bird> localFlockMates = Leader.Followers.Append(Leader as Bird)
            .OrderBy(b => Vector3.Distance(transform.position, b.transform.position))
            .Take(LocalFlockSize)
            .ToList();

        int flapCount = 0;
        float translationalSum = 0;
        Vector2 rotationalSum = Vector2.zero;

        foreach (var b in localFlockMates)
        {
            flapCount += b.FlapInput ? 1 : 0;
            translationalSum += b.TranslationalInput;
            rotationalSum += b.RotationalInput;
        }

        FlapInput = flapCount > LocalFlockSize / 2;
        TranslationalInput = translationalSum / LocalFlockSize;
        RotationalInput = rotationalSum / LocalFlockSize;
    }

    void OnCollisionEnter (Collision other)
    {
        FlockFollower otherBird = other.gameObject.GetComponent<FlockFollower>();

        if (otherBird == null || otherBird.Leader == Leader || otherBird.LeaderChangeTimer > 0) return;

        if (shouldSteal(other.collider) && !otherBird.shouldSteal(collider))
        {
            otherBird.SetLeader(Leader);
        }
    }

    public void SetLeader (FlockLeader newLeader)
    {
        if (LeaderChangeTimer > 0) return;

        Leader?.Followers?.Remove(this);
        newLeader.Followers.Add(this);
        Leader = newLeader;

        LeaderChangeTimer = LeaderChangeCooldown;
    }

    bool shouldSteal (Collider otherBirdCollider)
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.forward, out hit, 1000, StealCheckMask)) return false;

        return hit.collider == otherBirdCollider;
    }
}
