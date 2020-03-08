using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdStealer))]
public class FlockFollower : PhysicsBehaviour
{
    public float LeaderChangeTimer { get; private set; }

    public float LeaderChangeCooldown;
    public FlockLeader Leader;

    public float MaxSpeed;

    [Tooltip("Distance at which two boids consider themselves overly crowded")]
    public float CrowdedDistance;

    new Collider collider => GetComponent<Collider>();

    void Update ()
    {
        LeaderChangeTimer = Mathf.Max(LeaderChangeTimer - Time.deltaTime, 0);

        transform.rotation = Quaternion.Slerp
        (
            transform.rotation,
            Quaternion.LookRotation(Rigidbody.velocity),
            0.2f
        );
    }

    void FixedUpdate ()
    {
        var flock = Leader.Flock.Where(b => b != this);

        Rigidbody.velocity +=
            separationRule(flock) +
            alignmentRule(flock) +
            followLeaderRule();
    }

    public void SetLeader (FlockLeader newLeader)
    {
        if (LeaderChangeTimer > 0) return;

        Leader?.Followers?.Remove(this);
        newLeader.Followers.Add(this);
        Leader = newLeader;
        GetComponent<BirdStealer>().Leader = newLeader;

        transform.parent = newLeader.transform.parent;

        LeaderChangeTimer = LeaderChangeCooldown;
    }

    Vector3 separationRule (IEnumerable<PhysicsBehaviour> flock)
    {
        Vector3 separationVelocity = Vector3.zero;

        foreach (var boid in flock)
        {
            if (Vector3.Distance(boid.transform.position, transform.position) <= CrowdedDistance)
            {
                separationVelocity -= boid.transform.position - transform.position;
            }
        }

        return separationVelocity;
    }

    Vector3 alignmentRule (IEnumerable<PhysicsBehaviour> flock)
    {
        Vector3 perceivedAverageVelocity = Vector3.zero;

        foreach (var boid in flock)
        {
            perceivedAverageVelocity += boid.Rigidbody.velocity;
        }

        perceivedAverageVelocity /= flock.Count();

        // 8 is an arbitrary factor
        return (perceivedAverageVelocity - Rigidbody.velocity) / 8;
    }

    Vector3 followLeaderRule ()
    {
        // 8 is an arbitrary factor
        return (Leader.transform.position - transform.position) / 8;
    }
}
