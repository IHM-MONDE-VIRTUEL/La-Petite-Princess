using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TrigerManager trigerManager; // reference to the TrigerManager
    public GameObject portalObject; // reference to the portal object

    private bool allTriggersVisited = false; // flag to track whether all triggers have been visited

    private MeshRenderer portalMeshRenderer;
    private BoxCollider portalBoxCollider;
    
    
    void Start()
    {
        trigerManager.onAllTriggersVisited.AddListener(OnAllTriggersVisited);

        // Get the MeshRenderer and BoxCollider components from the portal object
        portalMeshRenderer = portalObject.GetComponent<MeshRenderer>();
        portalBoxCollider = portalObject.GetComponent<BoxCollider>();

        // Disable the portal object initially
        portalMeshRenderer.enabled = false;
        portalBoxCollider.enabled = false;
    }
    
      void OnAllTriggersVisited()
    {
        // Set the flag to indicate that all triggers have been visited
        allTriggersVisited = true;

        // Enable the portal object
        portalMeshRenderer.enabled = true;
        portalBoxCollider.enabled = true;
        portalBoxCollider.isTrigger = true;
        
        Debug.Log("Portal is now active!");
    }
    
    void OnTriggerEnter(Collider other){
        if (allTriggersVisited && other.gameObject.tag == "Player")
        {
            // Load the next scene
            Debug.Log("Triggerrr");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
