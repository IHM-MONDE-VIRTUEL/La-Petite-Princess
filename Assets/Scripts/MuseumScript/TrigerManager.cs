using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // needed to use UnityEvent
using UnityEngine.Video; // needed to use VideoPlayer


public class TrigerManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<Triger> trigers;
    private List<Triger> trigersVisisted = new List<Triger>();
    public int totalTrigers = 3;
    private int trigersActivated = 0;
    public UIManager ui;
    public UnityEvent onAllTriggersVisited; // new event to represent all triggers being visited

    
    void Start()
    {
        ListenToTrigers(true); 
        ui.UpdateQuestText("Find the mysteries from earth ! " + trigersActivated + " / " + totalTrigers);
          
    }
    
   private void ListenToTrigers(bool suscribe)
    {
        foreach (Triger triger in trigers)
        {
            if (suscribe) triger.onTrigEnter.AddListener(ActivateVideoPlayer);
            else triger.onTrigEnter.RemoveListener(ActivateVideoPlayer) ;
        }
    }


 public void ActivateVideoPlayer(GameObject player, Triger triger)
{
    // Check if the triger has been visited before
    if (trigersVisisted.Contains(triger)) return;
    // Find the VideoPlayer object in the scene based on the triggered triger's videoIndex
    VideoPlayer videoPlayer = null;
    if (triger.videoIndex == 1)
    {
        videoPlayer = GameObject.Find("VideoPlayer1").GetComponent<VideoPlayer>();
        Debug.Log("VideoPlayer1");
        trigersVisisted.Add(triger);
        
    }
    else if (triger.videoIndex == 2)
    {
        videoPlayer = GameObject.Find("VideoPlayer2").GetComponent<VideoPlayer>();
        Debug.Log("VideoPlayer2");
        trigersVisisted.Add(triger);
    }
    else if (triger.videoIndex == 3)
    {
        videoPlayer = GameObject.Find("VideoPlayer3").GetComponent<VideoPlayer>();
        Debug.Log("VideoPlayer3");
        trigersVisisted.Add(triger);
        
        
    }

    if (videoPlayer != null)
    {
        // Call the Play method on the VideoPlayer object
        Debug.Log("Play");
        videoPlayer.Play();
        trigersActivated++;
        ui.UpdateQuestText("Find the mysteries from earth ! " + trigersActivated + " / " + totalTrigers);
        CheckQuestStatus();
    }
}


void CheckQuestStatus()
    {
        if (trigersActivated == totalTrigers)
        {
            ListenToTrigers(false);
            ui.UpdateQuestText(
                "You're the chosen one, find the portal that will take you to a knowledgeable pilot from earth !"
            );
            onAllTriggersVisited.Invoke();

        }
    }

}
