using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 StartRotation;
    public float SmoothTime = 1f;

    private Vector3 targetPosition;
    private Vector3 targetRotation;
    private Vector3 posVelocity = Vector3.zero;
    private Vector3 rotVelocity = Vector3.zero;

    // Update is called once per frame
    void Start()
    {
        targetPosition = transform.position;
        transform.position = StartPosition;

        targetRotation = transform.eulerAngles;
        transform.eulerAngles = StartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref posVelocity, SmoothTime);
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetRotation, ref rotVelocity, SmoothTime);
    }
}
