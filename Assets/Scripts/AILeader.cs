using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

// TODO: hiding for a breather, sometimes switching after hitting target(?)
// TODO: be strategic sometimes - try to get on opponent's side, and only when that's achieved do you swoop in
public class AILeader : FlockLeader
{
    public Vector2 ChaseFrameTimeRange;
    public float MaximumSpeed, GiveUpDistance, DesiredMaximumDistance;
    [Range(0, 1)]
    public float PlayerGuaranteedTargetChance;
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
        if (RandomExtra.Chance(PlayerGuaranteedTargetChance))
        {
            target = FindObjectOfType(typeof(Player)) as FlockLeader;
            return;
        }

        var potentialTargets = (FindObjectsOfType(typeof(FlockLeader)) as FlockLeader[]).ToList();
        potentialTargets.Remove(this);

        Func<FlockLeader, bool> inRange = fl => Vector3.Distance(fl.transform.position, transform.position) <= DesiredMaximumDistance;

        if (!potentialTargets.Where(fl => fl != target).Any(inRange))
        {
            target = potentialTargets.PickRandom();
        }
        else
        {
            target = potentialTargets.Where(fl => fl != target).Where(inRange).ToList().PickRandom();
        }
    }
}
