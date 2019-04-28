using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject InfoScreen;
    public GameObject QuitButton;


    public void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            QuitButton.SetActive(false);
        }
    }
    
    public void ShowInfo()
    {
        InfoScreen.SetActive(true);
    }

    public void HideInfo()
    {
        InfoScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    
}
