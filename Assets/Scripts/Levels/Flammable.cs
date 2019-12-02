using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component for all flammable grid objects
public class Flammable : MonoBehaviour
{
    // Flags
    public bool isLit;
    public bool isDestructable;
    
    // References to VFX
    public GameObject Fire;
    public GameObject DestructionEffect;

    // Updates model to reflect state
    void Start()
    {
        if (Fire != null)
        {
            Fire.SetActive(GetComponent<Flammable>().isLit);
        }
    }

    // Updates model to reflect state
    void Update()
    {
        // Show/hide flame
        if (Fire != null)
        {
            Fire.SetActive(GetComponent<Flammable>().isLit);
        }
    }

    // Function to play an effect before destroying itself
    public void DestroyFlammableObject()
    {
        Instantiate(DestructionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Modifies the given grid to propogate fires to adjacent tiles
    public static GridObject[,] PropogateFire(GridObject[,] grid)
    {
        // Create 2D array to represent which tiles are on fire
        bool[,] fireGrid = new bool[grid.GetLength(0), grid.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                fireGrid[i, j] = false;

                if (grid[i, j] != null &&
                    grid[i, j].gameObject.GetComponent<Flammable>() != null &&
                    grid[i, j].gameObject.GetComponent<Flammable>().isLit)
                {
                    fireGrid[i, j] = grid[i, j].gameObject.GetComponent<Flammable>().isLit;

                    // Destroy burning stuff
                    if (grid[i, j].GetComponent<Flammable>().isDestructable)
                    {
                        grid[i, j].GetComponent<Flammable>().DestroyFlammableObject();
                        grid[i, j] = null;
                    }
                }
            }
        }

        // Propogate fire through firemap (adjacent tiles)
        for (int i = 0; i < fireGrid.GetLength(0); i++)
        {
            for (int j = 0; j < fireGrid.GetLength(1); j++)
            {
                if (fireGrid[i, j] == true)
                {
                    List<GridObject> adjacent = new List<GridObject>();
                    if (i > 0)
                    {
                        adjacent.Add(grid[i - 1, j]);
                    }
                    if (i < fireGrid.GetLength(0) - 1)
                    {
                        adjacent.Add(grid[i + 1, j]);
                    }
                    if (j > 0)
                    {
                        adjacent.Add(grid[i, j - 1]);
                    }
                    if (j < fireGrid.GetLength(1) - 1)
                    {
                        adjacent.Add(grid[i, j + 1]);
                    }
                    
                    foreach (GridObject obj in adjacent)
                    {
                        if (obj != null &&
                            obj.gameObject.GetComponent<Flammable>() != null)
                        {
                            obj.gameObject.GetComponent<Flammable>().isLit = true;
                        }
                    }
                }
            }
        }

        return grid;
    }

    // Stores the previous grid states of fire
    public static List<bool[,]> FireHistory = new List<bool[,]>();

    // Adds to the history stack
    public static void SaveFireGrid(GridObject[,] grid)
    {
        bool[,] fireGrid = new bool[grid.GetLength(0), grid.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                fireGrid[i, j] = false;

                if (grid[i, j] != null &&
                    grid[i, j].gameObject.GetComponent<Flammable>() != null)
                {
                    fireGrid[i, j] = grid[i, j].gameObject.GetComponent<Flammable>().isLit;
                }
            }
        }
        FireHistory.Add(fireGrid);
    }

    // Pops from the history stack
    public static bool[,] PopFireHistory()
    {
        bool[,] fireGrid = FireHistory[FireHistory.Count - 1];
        FireHistory.RemoveAt(FireHistory.Count - 1);
        return fireGrid;
    }
}
