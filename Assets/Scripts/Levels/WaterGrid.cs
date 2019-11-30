using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGrid : MonoBehaviour
{
    public PuzzleGrid pGrid;
    public static bool[,] Grid;

    void Start()
    {
        Grid = new bool[pGrid.gWidth, pGrid.gHeight];

        // Add all the grid objects in scene to the grid state
        foreach (WaterGridObject obj in GetComponentsInChildren<WaterGridObject>())
        {
            int[] gCoords = obj.GetGridCoordinates(pGrid.gWidth, pGrid.gHeight);
            Grid[gCoords[0], gCoords[1]] = true;
        }
    }

    public static void ExtinguishFires(GridObject[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridObject obj = grid[i, j];
                
                if (obj != null &&
                    obj.GetComponent<Flammable>() &&
                    obj.GetComponent<Flammable>().isLit &&
                    Grid[i, j])
                {
                    obj.GetComponent<Flammable>().isLit = false;
                }
            }
        }
    }
}
