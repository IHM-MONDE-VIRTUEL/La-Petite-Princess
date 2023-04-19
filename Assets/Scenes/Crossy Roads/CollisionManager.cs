using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public UITextMeshProManager uIManager;
    public Rigidbody rigiDepart;

    private void Start()
    {
        rigiDepart = transform.GetComponent<Rigidbody>(); // appeler Get une seul fois
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "vilain")
        {
            uIManager.UpdateTouchText(" MORT ");
            transform.position = new Vector3(4.22f, 0.76f, -77.27f);
            rigiDepart.angularVelocity = Vector3.zero;
            rigiDepart.velocity = Vector3.zero;
            StartCoroutine("Wait");
        }

        if (collision.gameObject.tag == "mur")
        {
            uIManager.UpdatePrevText(" Suivre la route ");
            StartCoroutine("Wait");
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        uIManager.UpdateTouchText("");
        uIManager.UpdatePrevText("");
    }
}
