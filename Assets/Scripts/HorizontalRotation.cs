﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRotation : MonoBehaviour
{
    public float speed = -11;

    void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.y += speed * Time.deltaTime;
        transform.eulerAngles = rot;
    }
}
