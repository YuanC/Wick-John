using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public static bool DogsAreSafe(GridObject[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridObject obj = grid[i, j];

                if (obj != null &&
                    obj.gameObject.tag == "Dog" &&
                    obj.GetComponent<Flammable>() &&
                    obj.GetComponent<Flammable>().isLit)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
