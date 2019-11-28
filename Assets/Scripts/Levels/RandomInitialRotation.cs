using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitialRotation : MonoBehaviour
{
    public GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        model.transform.Rotate(0, Random.value*360, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
