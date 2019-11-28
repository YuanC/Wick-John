﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGrid : MonoBehaviour
{
    // Dimensions of the grid
    public static int gWidth = 20;
    public static int gHeight = 16;

    private const int historySize = 1000; // Number of steps to save.

    public GridObject[,] grid = new GridObject[gWidth, gHeight];  // Grid representation of level
    private List<string[,]> history = new List<string[,]>();  // Only stores the object tags for recreation purposes

    // Maps prefab tags to their prefab objects
    public Dictionary<string, GameObject> GridObjectMap = new Dictionary<string, GameObject>();
    public List<GameObject> GridObjectPrefabs = new List<GameObject>();

    public LevelMenu levelMenu;

    public enum LevelState
    {
        Active = 0,
        Paused = 1,
        Done = 2
    }
    public LevelState levelState;

    // Start is called before the first frame update
    void Start()
    {
        levelState = LevelState.Active;

        // Create a map for all prefabs based on their tag
        foreach (GameObject obj in GridObjectPrefabs)
        {
            GridObjectMap.Add(obj.tag, obj);
        }

        // Add all the grid objects in scene to the grid state
        foreach (GridObject gridObj in GetComponentsInChildren<GridObject>())
        {
            int[] gCoords = gridObj.GetGridCoordinates(gWidth, gHeight);
            grid[gCoords[0], gCoords[1]] = gridObj;
        }
        SaveGridToHistory();
    }

    // Adds the current grid state to the undo stack
    void SaveGridToHistory()
    {
        string[,] newGrid = new string[gWidth, gHeight];

        for (int i = 0; i < gWidth; i++)
        {
            for (int j = 0; j < gHeight; j++)
            {
                if (grid[i, j] != null)
                {
                    newGrid[i, j] = grid[i, j].gameObject.tag;
                }
            }
        }
        history.Add(newGrid);
        Flammable.SaveFireGrid(grid);

        if (history.Count > historySize)
        {
            history.RemoveRange(historySize, history.Count - historySize);
        }

        //Debug.Log("New Grid added: " + history.Count + " / " + historySize);
        //PrintGrid();
    }

    // Prints the current grid state
    void PrintGrid()
    {
        for (int i = 0; i < gWidth; i++)
        {
            for (int j = 0; j < gHeight; j++)
            {
                if (grid[i, j] != null)
                {
                    Debug.Log(grid[i, j].gameObject.name);
                    GridObject.GetGlobalCoordinates(gWidth, gHeight, i, j);
                }
            }
        }
    }

    // Return a direction depending on input, or null if nothing is pressed
    string GetInputDirection()
    {
        string dir = null;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            dir = "up";
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = "left";
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            dir = "down";
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = "right";
        }
        return dir;
    }

    // Update is called once per frame
    void Update()
    {
        // Crappy but nonetheless working input handling implementation

        if (levelState != LevelState.Done && Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit Level
            SceneManager.LoadScene("Level Select");
        }
        else if (levelState != LevelState.Done && Input.GetKeyDown(KeyCode.R))
            {
            // Restarts level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            // Undo
            if (levelState != LevelState.Done && history.Count > 1)
            {
                string[,] prevGrid = history[history.Count - 1];
                history.RemoveAt(history.Count - 1);

                bool[,] fireGrid = Flammable.PopFireHistory(); // Saves state of fire for objects

                // Recreate Grid
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                for (int i = 0; i < gWidth; i++)
                { 
                    for (int j = 0; j < gHeight; j++)
                    {
                        grid[i, j] = null;

                        if (prevGrid[i, j] != null)
                        {
                            float[] coords = GridObject.GetGlobalCoordinates(gWidth, gHeight, i, j);
                            Vector3 pos = new Vector3(coords[0], 0, coords[1]);
                            GameObject prefab = GridObjectMap[prevGrid[i, j]];

                            GameObject obj = Instantiate(prefab, pos, Quaternion.identity, transform);
                            
                            if (obj.GetComponent<Flammable>() != null)
                            {
                                obj.GetComponent<Flammable>().isLit = fireGrid[i, j];
                            }

                            grid[i, j] = obj.GetComponent<GridObject>();
                        }
                    }
                }
                levelState = LevelState.Active;

                // Propogate message to movecounter in UI
                levelMenu.SendMessage("ModifyMoveCount", -1);
                levelMenu.SendMessage("SetFailMenu", false);
            }
        }
        else
        {
            string dir = GetInputDirection();

            if (levelState == LevelState.Active && dir != null)
            {
                // Calculate candle position after movement, null if no movement
                GridObject[,] newGrid = Candle.CalculateMovement(grid, dir);

                // Update Candle Position, if possible based on walls, level limits
                if (newGrid != null)
                {
                    SaveGridToHistory();
                    levelMenu.SendMessage("ModifyMoveCount", 1);

                    // Update flame propogation (candles, cobwebs), destruction
                    Flammable.PropogateFire(newGrid);

                    // Update wet tile interactions
                    WaterGrid.ExtinguishFires(newGrid);

                    // Calculate Puppies damage (Loss condition)
                    if (!Dog.DogsAreSafe(newGrid)){
                        levelState = LevelState.Paused;
                        levelMenu.SendMessage("SetFailMenu", true);
                    }

                    // Calculate Candles (Win/Loss condition)

                    if (Candle.AllCandlesLit(newGrid))
                    {
                        levelState = LevelState.Done;
                        levelMenu.SendMessage("SetCompleteMenu", true);
                    }

                    if (Candle.AllCandlesOut(newGrid))
                    {
                        levelState = LevelState.Paused;
                        levelMenu.SendMessage("SetFailMenu", true);
                    }

                    // Increase move counter
                    grid = newGrid;
                }
            }
        }
    }
}
