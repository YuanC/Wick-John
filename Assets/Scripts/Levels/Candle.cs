using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : GridObject
{
    public bool isLit = false;

    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Show/hide flame

        // Lerp toward target position
    }

    public static GridObject[,] CalculateMovement(GridObject[,] grid, string dir)
    {
        bool movementPossible = false;

        GridObject[,] newGrid = new GridObject[grid.GetLength(0), grid.GetLength(1)];
        // Scan depending on movement
        switch (dir)
        {
            case "up": // Scan from top -> bottom
                for (int j = 0; j < newGrid.GetLength(1); j++)
                {
                    for (int i = 0; i < newGrid.GetLength(0); i++)
                    {
                        if (j > 0 && grid[i, j - 1] == null) // Won't break boundaries and is blank tile
                        {
                            newGrid[i, j - 1] = grid[i, j];
                            newGrid[i, j] = null;
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }

                break;
            case "down": // Scan from bottom -> up
                for (int j = newGrid.GetLength(1) - 1; j >= 0; j++)
                {
                    for (int i = 0; i < newGrid.GetLength(0); i++)
                    {
                        if (j < newGrid.GetLength(1) - 1 && grid[i, j + 1] == null) // Won't break boundaries and is blank tile
                        {
                            newGrid[i, j + 1] = grid[i, j];
                            newGrid[i, j] = null;
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }

                break;
            case "left": // Scan from left -> right
                for (int i = 0; i < newGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < newGrid.GetLength(1); j++)
                    {

                    }
                }
                break;
            case "right": // Scan from right -> left
                for (int i = newGrid.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < newGrid.GetLength(1); j++)
                    {

                    }
                }
                break;
        }

        // Can only move when block is empty
        return movementPossible ? newGrid : null; 
    }
}
