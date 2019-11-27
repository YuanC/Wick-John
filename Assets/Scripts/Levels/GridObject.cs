using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    // Base object for all grid objects

    // Get the grid coordinates of this object depending on the dimensions of the grid
    public int[] GetGridCoordinates(int x, int y)
    {
        int[] gCoords = new int[2];
        gCoords[0] = (int)(transform.position.x + x/2);
        gCoords[1] = (int)(-transform.position.z + y/2);
        return gCoords;
    }

    // Transform the grid coordinates to global coordinates
    public static float[] GetGlobalCoordinates(int x, int y, int u, int v)
    {
        float[] gCoords = new float[2];
        gCoords[0] = ((float)u - x / 2f + 0.5f);
        gCoords[1] = ((float)(y-v-1) - y / 2f + 0.5f);
        Debug.Log(gCoords[0] + " " + gCoords[1]);
        return gCoords;
    }
}
