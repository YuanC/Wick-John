using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component for pushable grid objects
public class Pushable : MonoBehaviour
{
    // Checks if it is possible to push objects in a certain direction
    // The direction parameter should probably be set by an enum :P
    public static bool ObjectsPushable(GridObject[,] grid, int x, int y, string dir)
    {
        bool pushable = false;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        // iterates down a line of pushable objects until a free tile is found
        switch (dir)
        {
            case "up":
                for (int j = y - 1; j >= 0; j--)
                {
                    GridObject obj = grid[x, j];

                    if (obj == null)
                    {
                        pushable = true;
                        break;
                    }
                    else if (!obj.GetComponent<Pushable>())
                    {
                        break;
                    }
                }
                break;
            case "down":
                for (int j = y + 1; j < height; j++)
                {
                    GridObject obj = grid[x, j];

                    if (obj == null)
                    {
                        pushable = true;
                        break;
                    }
                    else if (!obj.GetComponent<Pushable>())
                    {
                        break;
                    }
                }
                break;
            case "left":
                for (int i = x - 1; i >= 0; i--)
                {
                    GridObject obj = grid[i, y];

                    if (obj == null)
                    {
                        pushable = true;
                        break;
                    }
                    else if (!obj.GetComponent<Pushable>())
                    {
                        break;
                    }
                }
                break;
            case "right":
                for (int i = x + 1; i < width; i++)
                {
                    GridObject obj = grid[i, y];

                    if (obj == null)
                    {
                        pushable = true;
                        break;
                    }
                    else if (!obj.GetComponent<Pushable>())
                    {
                        break;
                    }
                }
                break;
        }
        return pushable;
    }

    // Pushes all the pushable objects in the direction to make room for the candle
    public static void PushObjects(GridObject[,] grid, int x, int y, string dir)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        GridObject prev = null;

        // Shifts all grid objects by one in the specified direction
        switch (dir)
        {
            case "up":
                for (int j = y - 1; j >= 0; j--)
                {
                    if (prev != null)
                    {
                        Movable movable = prev.GetComponent<Movable>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, x, j);
                        movable.SetNewTargetPosition(new Vector3(coords[0], 0, coords[1]));
                    }
                    GridObject obj = grid[x, j];
                    grid[x, j] = prev;
                    if (obj == null)
                    {
                        break;
                    }
                    prev = obj;
                }
                break;
            case "down":
                for (int j = y + 1; j < height; j++)
                {
                    if (prev != null)
                    {
                        Movable movable = prev.GetComponent<Movable>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, x, j);
                        movable.SetNewTargetPosition(new Vector3(coords[0], 0, coords[1]));
                    }
                    GridObject obj = grid[x, j];
                    grid[x, j] = prev;
                    if (obj == null)
                    {
                        break;
                    }
                    prev = obj;
                }
                break;
            case "left":
                for (int i = x - 1; i >= 0; i--)
                {
                    if (prev != null)
                    {
                        Movable movable = prev.GetComponent<Movable>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, i, y);
                        movable.SetNewTargetPosition(new Vector3(coords[0], 0, coords[1]));
                    }
                    GridObject obj = grid[i, y];
                    grid[i, y] = prev;
                    if (obj == null)
                    {
                        break;
                    }
                    prev = obj;
                }
                break;
            case "right":
                for (int i = x + 1; i < width; i++)
                {
                    if (prev != null)
                    {
                        Movable movable = prev.GetComponent<Movable>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, i, y);
                        movable.SetNewTargetPosition(new Vector3(coords[0], 0, coords[1]));
                    }
                    GridObject obj = grid[i, y];
                    grid[i, y] = prev;
                    if (obj == null)
                    {
                        break;
                    }
                    prev = obj;
                }
                break;
        }
    }
}
