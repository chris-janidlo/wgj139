using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class PhysicsBehaviour : MonoBehaviour
{
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    public Collider Collider => GetComponent<Collider>();
}
