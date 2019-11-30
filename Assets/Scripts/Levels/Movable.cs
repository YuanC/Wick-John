using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public Vector3 prevRotation;

    public GameObject model;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public float moveSpeed = 10;

    public bool RandomRotationOnMove = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;

        if (RandomRotationOnMove)
        {
            targetRotation = new Vector3(0, Random.value * 360, 0);
            prevRotation = model.transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Travel, rotate toward target position
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

    public void SetNewTargetPosition(Vector3 position)
    {
        targetPosition = position;
        prevRotation = targetRotation;
        targetRotation = new Vector3(0, targetRotation.y + Random.value * 90 - 45, 0);
    }
}
