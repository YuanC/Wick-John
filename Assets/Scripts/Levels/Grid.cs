using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    private const int gWidth = 20;
    private const int gHeight = 16;

    public GridObject[,] grid = new GridObject[gWidth, gHeight];
    private Stack<string[,]> history = new Stack<string[,]>();  // Only stores the object tag

    private float transitionTimer = 0.0f;
    public float TransitionTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // Parse the scene and get all the shit loaded into GridObject
        foreach (Transform trans in transform)
        {
            if (trans.gameObject.GetComponent<GridObject>())
            {
                int[] gCoords = trans.gameObject.GetComponent<GridObject>()?.GetGridCoordinates(gWidth, gHeight);
                grid[gCoords[0],gCoords[1]] = trans.gameObject.GetComponent<GridObject>();
            }
        }
        SaveGridToHistory();
    }

    void SaveGridToHistory()
    {


        PrintGrid();
    }


    void PrintGrid()
    {
        for (int i = 0; i < gWidth; i++)
        {
            for (int j = 0; j < gHeight; j++)
            {
                if (grid[i,j] != null)
                {
                    Debug.Log(grid[i, j].gameObject.name);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transitionTimer -= Time.deltaTime;
        if (transitionTimer > 0)
        {
            // Input with cooldown

            GridObject[,] newGrid = new GridObject[gWidth, gHeight];

            // Update Candle Position, if possible based on walls, level limits

            //Increase move counter

            // Update flame propogation (candles, cobwebs)
            // Cobwebs destruction
            // Calculate wet/rain tiles
            // Calculate Puppies
            // 
        }
        else
        {
            transitionTimer = TransitionTime;
        }
    }
}
