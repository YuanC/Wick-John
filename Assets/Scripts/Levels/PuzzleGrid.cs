using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private const int gWidth = 20;
    private const int gHeight = 16;

    private GridObject[,] grid = new GridObject[gWidth, gHeight];
    private List<GridObject[,]> history = new List<GridObject[,]>();

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
                Debug.Log("child" + gCoords[0] + ", " + gCoords[1]);
                grid[gCoords[0],gCoords[1]] = trans.gameObject.GetComponent<GridObject>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
