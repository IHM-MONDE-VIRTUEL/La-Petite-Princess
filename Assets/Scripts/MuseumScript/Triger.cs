using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // needed to use UnityEvent


public class Triger : MonoBehaviour
{
    public int videoIndex;
    public UnityEvent<GameObject, Triger> onTrigEnter;

    
 void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onTrigEnter.Invoke(other.gameObject, this);
        }
    }
}
