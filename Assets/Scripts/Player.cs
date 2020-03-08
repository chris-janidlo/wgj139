using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FlockLeader
{
    public float MaxSpeed, TurnSpeed;

    Vector2 targetRotation;
    public string FlapButton, TranslationalAxis, PitchAxis, YawAxis;
    public bool InversePitch = true;

    bool flapInput { get; set; }
    // scalar for z force on flap (ie you move forward a little on flap when this is positive, or back a little when negative)
    float translationalInput { get; set; }
    // x is yaw, y is pitch
    Vector2 rotationalInput { get; set; }

    void Update ()
    {
        flapInput = Input.GetButton(FlapButton);
        translationalInput = Input.GetAxis(TranslationalAxis);
        rotationalInput = new Vector2
        (
            Input.GetAxis(YawAxis),
            Input.GetAxis(PitchAxis) * (InversePitch ? -1 : 1)
        );

        Debug.Log(Followers.Count);
    }

    void FixedUpdate ()
    {
        rotate();
        translate();
    }

    void rotate ()
    {
        targetRotation += rotationalInput * TurnSpeed;
        targetRotation = new Vector2
        (
            targetRotation.x % 360,
            Mathf.Clamp(targetRotation.y, -90, 90)
        );

        transform.localRotation = Quaternion.AngleAxis(targetRotation.y, Vector3.right);
        transform.localRotation *= Quaternion.AngleAxis(targetRotation.x, transform.InverseTransformDirection(Vector3.up));
    }

    void translate ()
    {
        // TODO: bird physics
        Rigidbody.velocity = transform.forward * MaxSpeed;
    }
}
