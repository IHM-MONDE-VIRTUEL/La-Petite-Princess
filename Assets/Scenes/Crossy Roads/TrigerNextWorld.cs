using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerNextWorld : MonoBehaviour
{
    public LvlLoader lvlLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lvlLoader.LoadNextLevel();
        }
    }
}
