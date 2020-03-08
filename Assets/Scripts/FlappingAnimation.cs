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
        int i = Random.Range(0, Frames.Count);

        yield return new WaitForSeconds(Random.Range(0, Frames.Count / (FPS * SpeedModifier)));

        while (true)
        {
            MeshFilter.mesh = Frames[i];
            i = (i + 1) % Frames.Count;

            yield return new WaitForSeconds(1 / (FPS * SpeedModifier));
        }
    }
}
