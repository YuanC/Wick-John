﻿using System.Collections;
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
        transform.position = targetPosition;
    }

    // Determines the candles' new positions, if possible
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
                        if (gObj != null &&
                            gObj.gameObject.tag == "Candle" &&
                            j > 0 &&
                            grid[i, j - 1] == null) // Won't break boundaries and is blank tile
                        {
                            newGrid[i, j - 1] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i, j - 1);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Candle candle = gObj.GetComponent<Candle>();
                            candle.targetPosition = newPos;
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
                        GridObject gObj = grid[i, j];
                        if (gObj != null &&
                            gObj.gameObject.tag == "Candle" && 
                            j < y - 1 &&
                            grid[i, j + 1] == null) // Won't break boundaries and is blank tile
                        {
                            newGrid[i, j + 1] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i, j + 1);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Candle candle = gObj.GetComponent<Candle>();
                            candle.targetPosition = newPos;
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
                            gObj.gameObject.tag == "Candle" &&
                            i > 0 &&
                            grid[i - 1, j] == null) // Won't break boundaries and is blank tile
                        {
                            newGrid[i - 1, j] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i - 1, j);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Candle candle = gObj.GetComponent<Candle>();
                            candle.targetPosition = newPos;
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
                            gObj.gameObject.tag == "Candle" &&
                            i < x - 1 &&
                            grid[i + 1, j] == null) // Won't break boundaries and is blank tile
                        {
                            newGrid[i + 1, j] = gObj;
                            newGrid[i, j] = null;

                            float[] xz = GridObject.GetGlobalCoordinates(x, y, i + 1, j);
                            Vector3 newPos = new Vector3(xz[0], 0, xz[1]);

                            Candle candle = gObj.GetComponent<Candle>();
                            candle.targetPosition = newPos;
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
