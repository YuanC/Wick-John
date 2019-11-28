using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public bool isLit;
    public GameObject Fire;

    // Start is called before the first frame update
    void Start()
    {
        if (Fire != null)
        {
            Fire.SetActive(GetComponent<Flammable>().isLit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show/hide flame
        if (Fire != null)
        {
            Fire?.SetActive(GetComponent<Flammable>().isLit);
        }
    }

    public static GridObject[,] PropogateFire(GridObject[,] grid)
    {
        // Create FireMap
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
                    if (grid[i, j].gameObject.tag == "Wood")
                    {
                        Destroy(grid[i, j].gameObject);
                        grid[i, j] = null;
                    }
                }
            }
        }

        // Propogate fire through firemap
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

    public static List<bool[,]> FireHistory = new List<bool[,]>();

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

    public static bool[,] PopFireHistory()
    {
        bool[,] fireGrid = FireHistory[FireHistory.Count - 1];
        FireHistory.RemoveAt(FireHistory.Count - 1);
        return fireGrid;
    }
}
