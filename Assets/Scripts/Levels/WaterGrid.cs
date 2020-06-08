using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2D array representing which tiles are wet
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

    // Applies the watergrid to the main grid in order to extinguish lit grid objects
    public static void ExtinguishFires(GridObject[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridObject obj = grid[i, j];
                
                // Performance: we should probably make this into a seperate <Flammable> grid so we don't ahve to call GetComponent() each frame
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
