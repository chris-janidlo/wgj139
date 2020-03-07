using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public string FlapButton, TranslationalAxis, PitchAxis, YawAxis;
    public bool InversePitch = true;
    public Bird Birb;

    void Update ()
    {
        Birb.FlapInput = Input.GetButton(FlapButton);
        Birb.TranslationalInput = Input.GetAxis(TranslationalAxis);
        Birb.RotationalInput = new Vector2
        (
            Input.GetAxis(YawAxis),
            Input.GetAxis(PitchAxis) * (InversePitch ? -1 : 1)
        );
    }
}
