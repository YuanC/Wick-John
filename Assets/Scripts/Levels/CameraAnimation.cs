using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 TargetPosition;
    public float smoothTime = 1F;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Start()
    {
        transform.position = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref velocity, smoothTime);
    }
}
