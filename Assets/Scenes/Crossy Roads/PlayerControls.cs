using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    private float inputX;
    private float inputY;
    private Vector2 inputV;
    public UnityEvent<Vector2> onInput;
    
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        inputV = new Vector2(inputX, inputY).normalized;
        onInput.Invoke(inputV);
    }
}
