using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
  
    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (getKey("w"))
            movement.z = 1.0f;
        if (getKey("a"))
            movement.x = 1.0f;
        if (getKey("d"))
            movement.y = 1.0f;
        gameObject.transform.position += movement;
    }
}
