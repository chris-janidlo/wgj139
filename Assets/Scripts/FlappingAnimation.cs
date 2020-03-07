using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappingAnimation : MonoBehaviour
{
    public float SpeedModifier { get; set; } = 1;

    public float FPS;
    public List<Mesh> Frames;
    public MeshFilter MeshFilter;

    IEnumerator Start ()
    {
        int i = 0;

        while (true)
        {
            yield return new WaitForSeconds(1 / (FPS * SpeedModifier));

            MeshFilter.mesh = Frames[i];
            i = (i + 1) % Frames.Count;
        }
    }
}
