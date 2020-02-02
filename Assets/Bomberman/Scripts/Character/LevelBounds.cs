using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    public int width;
    public int height;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(width, 0, 0));
        Gizmos.DrawLine(new Vector3(width, 0, 0), new Vector3(width, height, 0));
        Gizmos.DrawLine(new Vector3(width,height, 0), new Vector3(0, height, 0));
        Gizmos.DrawLine(new Vector3(0, height, 0), new Vector3(0, 0, 0));
    }
}
