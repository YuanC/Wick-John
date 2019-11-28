using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Vector3 prevRotation;

    public GameObject model;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        targetRotation = new Vector3(0, Random.value*360, 0);
        prevRotation = model.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Travel, rotate toward target position
        Vector3 diff = targetPosition - transform.position;

        if (diff.magnitude != 0)
        {
            model.transform.eulerAngles = Vector3.Slerp(prevRotation, targetRotation, 1 - diff.magnitude);
        }
        else
        {
            model.transform.eulerAngles = targetRotation;
        }

        diff.Normalize();
        transform.position += (diff * Mathf.Min(moveSpeed * Time.deltaTime, diff.magnitude));

        Vector3 newDiff = targetPosition - transform.position;
        newDiff.Normalize();

        if (Vector3.Dot(diff, newDiff) < 0)
        {
            transform.position = targetPosition;
        }
    }

    public static bool ChairsPushable(GridObject[,] grid, int x, int y, string dir)
    {
        bool pushable = false;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

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
                    else if (obj.gameObject.tag != "Chair")
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
                    else if (obj.gameObject.tag != "Chair")
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
                    else if (obj.gameObject.tag != "Chair")
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
                    else if (obj.gameObject.tag != "Chair")
                    {
                        break;
                    }
                }
                break;
        }
        return pushable;
    }

    // Pushes all the chairs in the direction to make room for the candle
    public static void PushChairs(GridObject[,] grid, int x, int y, string dir)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        GridObject prev = null;

        switch (dir)
        {
            case "up":
                for (int j = y - 1; j >= 0; j--)
                {
                    if (prev != null)
                    {
                        Chair chair = prev.GetComponent<Chair>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, x, j);
                        chair.targetPosition = new Vector3(coords[0], 0, coords[1]);
                        chair.prevRotation = chair.targetRotation;
                        chair.targetRotation = new Vector3(0, chair.targetRotation.y + Random.value * 90 - 45, 0);
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
                        Chair chair = prev.GetComponent<Chair>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, x, j);
                        chair.targetPosition = new Vector3(coords[0], 0, coords[1]);
                        chair.prevRotation = chair.targetRotation;
                        chair.targetRotation = new Vector3(0, chair.targetRotation.y + Random.value * 90 - 45, 0);
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
                        Chair chair = prev.GetComponent<Chair>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, i, y);
                        chair.targetPosition = new Vector3(coords[0], 0, coords[1]);
                        chair.prevRotation = chair.targetRotation;
                        chair.targetRotation = new Vector3(0, chair.targetRotation.y + Random.value * 90 - 45, 0);
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
                        Chair chair = prev.GetComponent<Chair>();
                        float[] coords = GridObject.GetGlobalCoordinates(width, height, i, y);
                        chair.targetPosition = new Vector3(coords[0], 0, coords[1]);
                        chair.prevRotation = chair.targetRotation;
                        chair.targetRotation = new Vector3(0, chair.targetRotation.y + Random.value * 90 - 45, 0);
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
