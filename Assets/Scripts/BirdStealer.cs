using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdStealer : PhysicsBehaviour
{
    public FlockLeader Leader;

    public LayerMask StealCheckMask;

    void OnCollisionEnter (Collision other)
    {
        FlockFollower otherBird = other.gameObject.GetComponent<FlockFollower>();

        if (otherBird == null || otherBird.Leader == Leader || otherBird.LeaderChangeTimer > 0) return;

        if (shouldSteal(transform, other.collider) && !shouldSteal(other.transform, Collider))
        {
            otherBird.SetLeader(Leader);
        }
    }

    bool shouldSteal (Transform thisBirdTransform, Collider otherBirdCollider)
    {
        RaycastHit hit;

        if (!Physics.Raycast(thisBirdTransform.position, thisBirdTransform.forward, out hit, 1000, StealCheckMask)) return false;

        return hit.collider == otherBirdCollider;
    }
}
