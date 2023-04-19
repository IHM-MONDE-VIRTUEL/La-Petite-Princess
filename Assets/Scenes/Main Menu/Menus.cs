using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public UIDocument uiDocument;

    // Start is called before the first frame update
    void Start()
    {
        this.uiDocument.rootVisualElement.Q<Button>("All").RegisterCallback<ClickEvent>((evt) => SceneManager.LoadScene("Museum"));
        this.uiDocument.rootVisualElement.Q<Button>("Museum").RegisterCallback<ClickEvent>((evt) => SceneManager.LoadScene("Museum"));
        this.uiDocument.rootVisualElement.Q<Button>("CrossyRoads").RegisterCallback<ClickEvent>((evt) => SceneManager.LoadScene("Crossy Roads"));
        this.uiDocument.rootVisualElement.Q<Button>("Tycoon").RegisterCallback<ClickEvent>((evt) => SceneManager.LoadScene("Tycoon"));
    }
}
