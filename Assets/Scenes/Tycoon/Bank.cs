using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public GameEngine gameEngine;
    private int frameCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        this.gameEngine.collectBank();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (this.frameCount++ < GameEngine.MAX_VISUAL_UPDATE_FRAMES) return;
        this.frameCount = 0;
        
        this.gameEngine.collectBank();
    }
}
