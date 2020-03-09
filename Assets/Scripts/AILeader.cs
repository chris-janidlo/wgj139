using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

// TODO: hiding for a breather, sometimes switching after hitting target(?)
public class AILeader : FlockLeader
{
    public Vector2 ChaseFrameTimeRange;
    public float MaximumSpeed, GiveUpDistance, DesiredMaximumDistance;
    public int Patience;

    FlockLeader target;
    int patienceTicker;

    void FixedUpdate ()
    {
        if (target == null)
        {
            switchTarget();
        }
        else
        {
            Rigidbody.velocity += (target.transform.position - transform.position) / 100;
        }

        if (Rigidbody.velocity.magnitude > MaximumSpeed)
            Rigidbody.velocity = Rigidbody.velocity.normalized * MaximumSpeed;
    }

    IEnumerator behaviorRoutine ()
    {
        while (true)
        {
            if (shouldSwitchTarget()) switchTarget();

            patienceTicker--;

            yield return new WaitForSeconds(RandomExtra.Range(ChaseFrameTimeRange));
        }
    }

    bool shouldSwitchTarget ()
    {
        return
            target == null ||
            Vector3.Distance(transform.position, target.transform.position) >= GiveUpDistance ||
            patienceTicker <= 0;
    }

    void switchTarget ()
    {
        var potentialTargets = (FindObjectsOfType(typeof(FlockLeader)) as FlockLeader[]).ToList();
        potentialTargets.Remove(this);

        Func<FlockLeader, bool> inRange = fl => Vector3.Distance(fl.transform.position, transform.position) <= DesiredMaximumDistance;

        if (!potentialTargets.Any(inRange))
        {
            target = potentialTargets.PickRandom();
        }
        else
        {
            target = potentialTargets.Where(inRange).ToList().PickRandom();
        }
    }
}
