using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class CameraFlockZoom : MonoBehaviour
{
    public AnimationCurve ZOffsetByFollowerCount;
    public TransitionableFloat ZOffsetTransition;

    public Player Player;

    Vector3 initialPosition;

    void Start ()
    {
        initialPosition = transform.position;
        ZOffsetTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        ZOffsetTransition.StartTransitionToIfNotAlreadyStarted
        (
            ZOffsetByFollowerCount.Evaluate(Player.Followers.Count)
        );

        transform.localPosition = initialPosition + Vector3.back * ZOffsetTransition.Value;
    }
}
