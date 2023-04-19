using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCheckpoint : MonoBehaviour
{
    public UnityEvent<GameObject, SimpleCheckpoint> onCheckpointEnter;

    private void OnTriggerEnter(Collider collinder)
    {
        if (collinder.gameObject.tag == "Player")
        {
            onCheckpointEnter.Invoke(collinder.gameObject, this);
        }
    }
}
