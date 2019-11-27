using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    // Get the grid coordinates of this object depending on the dimensions of the grid
    public int[] GetGridCoordinates(int x, int y)
    {
        int[] gCoords = new int[2];
        gCoords[0] = (int)(transform.position.x + x/2);
        gCoords[1] = (int)(-transform.position.z + y/2);
        return gCoords;
    }
}
