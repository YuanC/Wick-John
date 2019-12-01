using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private Vector3 target = new Vector3(0.0f, 1.5f, 0.0f);
    public float Speed = 5f;

    void Update()
    {
        // Spin the object around the target position
        transform.RotateAround(target, Vector3.up, Speed * Time.deltaTime);
    }
}
