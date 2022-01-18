using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuButtonGroup;
    bool clicked = true;

    private void Start() {
        menuButtonGroup.SetActive(false);
    }

    public void MenuButtonClicked()
    {
        if (!clicked){ 
            menuButtonGroup.SetActive(false); 
            clicked = true;
        }
        else { 
            menuButtonGroup.SetActive(true);
            clicked = false; 
        }
    }
}
