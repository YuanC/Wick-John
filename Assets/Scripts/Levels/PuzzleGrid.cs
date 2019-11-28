using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGrid : MonoBehaviour
{
    // Dimensions of the grid
    private const int gWidth = 20;
    private const int gHeight = 16;

    private const int historySize = 200; // Number of steps to save

    public GridObject[,] grid = new GridObject[gWidth, gHeight];  // Grid representation of level
    private List<string[,]> history = new List<string[,]>();  // Only stores the object tags for recreation purposes

    // Adds cooldown between movement actions to make room for animations, etc...
    private float coolDownTimer = 0.0f;
    public float CooldownDuration = 0.2f;

    // Maps prfab tags to their prefab objects
    public Dictionary<string, GameObject> GridObjectMap;

    public enum LevelState
    {
        Active = 0,
        Paused = 1
    }
    public LevelState levelState;

    // Start is called before the first frame update
    void Start()
    {
        levelState = LevelState.Active;

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
        Debug.Log("New Grid added: " + history.Count + " / " + historySize);
        PrintGrid();
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
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            // Crappy but nonetheless working input handling implementation

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Exit Level
                SceneManager.LoadScene("Level Select");
                coolDownTimer = CooldownDuration;
            }
            else if (Input.GetKeyDown(KeyCode.R))
                {
                // Restarts level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                coolDownTimer = CooldownDuration;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                // Undo
                string[,] prevGrid = history[history.Count - 1];
                history.RemoveAt(history.Count - 1);
                coolDownTimer = CooldownDuration;

                // TODO: Propogate message to movecounter in UI
            }
            else
            {
                string dir = GetInputDirection();

                if (dir != null)
                {
                    // Calculate candle position after movement, null if no movement
                    GridObject[,] newGrid = Candle.CalculateMovement(grid, dir);

                    // Update Candle Position, if possible based on walls, level limits
                    if (newGrid != null)
                    {
                        // Update flame propogation (candles, cobwebs)
                        // Cobwebs destruction
                        // Calculate wet/rain tiles affecting candles
                        // Calculate Puppies
                        // Win/Loss condition

                        // Add to history remove excess states if exceeds limit
                        // Increase move counter

                        coolDownTimer = CooldownDuration;
                    }
                }
            }
        }
    }
}
