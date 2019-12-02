using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interpolated animation for the camera upon the start of each level
public class CameraAnimation : MonoBehaviour
{
    // Parameters for beginning and ending transform values
    public Vector3 StartPosition;
    public Vector3 StartRotation;
    public float SmoothTime = 1f;

    private Vector3 targetPosition;
    private Vector3 targetRotation;
    private Vector3 posVelocity = Vector3.zero;
    private Vector3 rotVelocity = Vector3.zero;

    void Start()
    {
        targetPosition = transform.position;
        transform.position = StartPosition;

        targetRotation = transform.eulerAngles;
        transform.eulerAngles = StartRotation;
    }

    void Update()
    {
        // Interpolates per frame
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref posVelocity, SmoothTime);
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetRotation, ref rotVelocity, SmoothTime);
    }
}
