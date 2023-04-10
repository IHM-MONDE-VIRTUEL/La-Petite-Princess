using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TextMeshPro text;
    
    public void UpdateQuestText(string message){
        text.text = message;
    }
    

}
