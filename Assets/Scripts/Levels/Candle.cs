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

    public static GridObject[,] CalculateMovement(GridObject[,] grid, KeyCode dir)
    {
        // Scan depending on movement

        // Can only move when block is empty
        return null;
    }
}
