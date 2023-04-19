using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarMovement : MonoBehaviour
{
    private Vector2 inputV;
    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInput(Vector2 input)
    {
        inputV = input;
    }
    
    private void FixedUpdate()
    {
        float speed = inputV.y > 0 ? forwardMoveSpeed : -backwardMoveSpeed;
        if (inputV.y == 0) speed = 0;
        rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);

        float rotation = inputV.x * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }
}
