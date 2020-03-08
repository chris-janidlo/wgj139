using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Bird : MonoBehaviour
{
    public bool FlapInput { get; set; }
    // scalar for z force on flap (ie you move forward a little on flap when this is positive, or back a little when negative)
    public float TranslationalInput { get; set; }
    // x is yaw, y is pitch
    public Vector2 RotationalInput { get; set; }

    public float MaxSpeed, TurnSpeed;

    public Rigidbody Rigidbody;

    Vector2 targetRotation;

    void FixedUpdate ()
    {
        rotate();
        translate();
    }

    void rotate ()
    {
        targetRotation += RotationalInput * TurnSpeed;
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
