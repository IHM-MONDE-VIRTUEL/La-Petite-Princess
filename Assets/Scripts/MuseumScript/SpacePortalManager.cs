using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpacePortalManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public TrigerManager trigerManager; // reference to the TrigerManager
    public GameObject portalObject; // reference to the portal object
    private bool allTriggersVisited = false; // flag to track whether all triggers have been visited
    public MeshRenderer portalMeshRenderer1;
    public MeshRenderer portalMeshRenderer2;
    private BoxCollider portalBoxCollider;
    public AudioSource portalSound;
    public LvlLoader lvlLoader;


    void Start()
    {
        trigerManager.onAllTriggersVisited.AddListener(OnAllTriggersVisited);
        portalBoxCollider = portalObject.GetComponent<BoxCollider>();
        portalMeshRenderer1.enabled = false;
        portalMeshRenderer2.enabled = false;
        portalBoxCollider.enabled = false;
    }

    void OnAllTriggersVisited()
    {
        // Set the flag to indicate that all triggers have been visited
        allTriggersVisited = true;

        // Enable the portal object
        portalMeshRenderer1.enabled = true;
        portalMeshRenderer2.enabled = true;
        portalBoxCollider.enabled = true;
        portalBoxCollider.isTrigger = true;
        portalSound.Play();

        Debug.Log("Portal is now active! and triggered");
    }


    void OnTriggerEnter(Collider other)
    {
        if (allTriggersVisited && other.gameObject.tag == "Player")
        {
            // Load the next scene
            lvlLoader.LoadNextLevel();
        }
    }
}
