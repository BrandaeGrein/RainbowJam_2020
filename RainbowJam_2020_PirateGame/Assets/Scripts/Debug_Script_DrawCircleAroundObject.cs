using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a very simple script that draws a circle around an object -- purely for debugging purposes. 
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class Debug_Script_DrawCircleAroundObject : MonoBehaviour
{

    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        DrawCircle();
    }

    private void DrawCircle()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            z = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}
