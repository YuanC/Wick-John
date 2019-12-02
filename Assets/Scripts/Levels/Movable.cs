using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component for all movable objects.
public class Movable : MonoBehaviour
{
    public Vector3 prevRotation;

    public GameObject model;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public float moveSpeed = 10;

    public bool RandomRotationOnMove = false;   // For chairs, which swivel when pushed

    void Start()
    {
        targetPosition = transform.position;

        if (RandomRotationOnMove)
        {
            targetRotation = new Vector3(0, Random.value * 360, 0);
            prevRotation = model.transform.eulerAngles;
        }
    }

    // Travel, rotate toward target position
    void Update()
    {
        Vector3 diff = targetPosition - transform.position;

        if (RandomRotationOnMove)
        {
            if (diff.magnitude != 0)
            {
                model.transform.eulerAngles = Vector3.Slerp(prevRotation, targetRotation, 1 - diff.magnitude);
            }
            else
            {
                model.transform.eulerAngles = targetRotation;
            }
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

    // Set a new destination/rotation to interpolate to
    public void SetNewTargetPosition(Vector3 position)
    {
        targetPosition = position;
        prevRotation = targetRotation;
        targetRotation = new Vector3(0, targetRotation.y + Random.value * 90 - 45, 0);
    }
}
