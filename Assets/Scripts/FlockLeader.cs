using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlockLeader : PhysicsBehaviour
{
    public IEnumerable<PhysicsBehaviour> Flock => Followers.Append(this as PhysicsBehaviour);

    public UnityEvent Died;

    public List<FlockFollower> Followers = new List<FlockFollower>();
    public float InitialMaxRadius;

    protected virtual void Update ()
    {
        if (Followers.Count == 0)
        {
            Destroy(gameObject);
            Died?.Invoke();
            return;
        }
    }
}
