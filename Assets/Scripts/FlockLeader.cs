using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlockLeader : Bird
{
    public UnityEvent Died;

    public List<FlockFollower> Followers;
    public float InitialMaxRadius;

    void Update ()
    {
        if (Followers.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
