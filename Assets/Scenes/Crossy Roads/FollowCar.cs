using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform player;
    public Vector3 marginFromPlayer;
    public Animator cameraIntroAnimator;
    private void Awake()
    {
        StartIntro();
    }

    public void StartIntro()
    {
        cameraIntroAnimator.enabled = true;
        StartCoroutine("WAT");
    }

    IEnumerator WAT()
    {
        yield return new WaitForSeconds(9);
        cameraIntroAnimator.enabled = false;
    }
    void Update()
    {
        if (!cameraIntroAnimator.enabled)
        {
            transform.position = player.transform.position + marginFromPlayer;
        }
    }
}
