using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component for candles
public class Candle : MonoBehaviour
{
    // Determines from grid if all the candles were on fire
    public static bool AllCandlesLit(GridObject[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridObject obj = grid[i, j];

                if (obj != null &&
                    obj.GetComponent<Candle>() &&
                    obj.GetComponent<Flammable>() &&
                    !obj.GetComponent<Flammable>().isLit)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Determines from grid if all the candles are unlit
    public static bool AllCandlesOut(GridObject[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridObject obj = grid[i, j];

                if (obj != null &&
                    obj.GetComponent<Candle>() &&
                    obj.GetComponent<Flammable>() &&
                    obj.GetComponent<Flammable>().isLit)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
