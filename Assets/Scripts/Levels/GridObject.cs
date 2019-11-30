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
        return gCoords;
    }

    // Determines the candles' new positions, if possible.
    // I'm not too proud of this function...
    public static GridObject[,] CalculateMovement(GridObject[,] grid, string dir)
    {
        bool movementPossible = false;

        int x = grid.GetLength(0);
        int y = grid.GetLength(1);

        GridObject[,] newGrid = new GridObject[x, y];

        // Scan depending on movement
        switch (dir)
        {
            case "up": // Scan from top -> bottom
                for (int j = 0; j < y; j++)
                {
                    for (int i = 0; i < x; i++)
                    {
                        GridObject gObj = grid[i, j];

                        // Conditions for movement
                        if (gObj != null &&
                            gObj.GetComponent<Candle>() &&
                            j > 0 &&
                            gObj.GetComponent<Flammable>().isLit &&
                            Pushable.ObjectsPushable(newGrid, i, j, dir))
                        {
                            Pushable.PushObjects(newGrid, i, j, dir);

                            newGrid[i, j - 1] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i, j - 1);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Movable movable = gObj.GetComponent<Movable>();
                            movable.SetNewTargetPosition(newPos);
                            movementPossible = true;
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }
                break;

            case "down": // Scan from bottom -> up
                for (int j = y - 1; j >= 0; j--)
                {
                    for (int i = 0; i < x; i++)
                    {
                        // Conditions for movement
                        GridObject gObj = grid[i, j];
                        if (gObj != null &&
                            gObj.GetComponent<Candle>() &&
                            j < y - 1 &&
                            gObj.GetComponent<Flammable>().isLit &&
                            Pushable.ObjectsPushable(newGrid, i, j, dir))
                        {
                            Pushable.PushObjects(newGrid, i, j, dir);

                            newGrid[i, j + 1] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i, j + 1);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Movable movable = gObj.GetComponent<Movable>();
                            movable.SetNewTargetPosition(newPos);
                            movementPossible = true;
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }
                break;

            case "left": // Scan from left -> right
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        GridObject gObj = grid[i, j];
                        if (gObj != null &&
                            gObj.GetComponent<Candle>() &&
                            i > 0 &&
                            gObj.GetComponent<Flammable>().isLit &&
                            Pushable.ObjectsPushable(newGrid, i, j, dir)) // Won't break boundaries and is blank tile
                        {
                            Pushable.PushObjects(newGrid, i, j, dir);

                            newGrid[i - 1, j] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i - 1, j);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Movable movable = gObj.GetComponent<Movable>();
                            movable.SetNewTargetPosition(newPos);
                            movementPossible = true;
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }
                break;

            case "right": // Scan from right -> left
                for (int i = x - 1; i >= 0; i--)
                {
                    for (int j = 0; j < y; j++)
                    {
                        GridObject gObj = grid[i, j];
                        if (gObj != null &&
                            gObj.GetComponent<Candle>() &&
                            i < x - 1 &&
                            gObj.GetComponent<Flammable>().isLit &&
                            Pushable.ObjectsPushable(newGrid, i, j, dir)) // Won't break boundaries and is blank tile
                        {
                            Pushable.PushObjects(newGrid, i, j, dir);

                            newGrid[i + 1, j] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i + 1, j);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Movable movable = gObj.GetComponent<Movable>();
                            movable.SetNewTargetPosition(newPos);
                            movementPossible = true;
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }
                break;
        }

        // Can only move when block is empty
        return movementPossible ? newGrid : null;
    }
}
