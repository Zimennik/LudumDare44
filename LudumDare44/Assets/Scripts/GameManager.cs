using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player Player;
    public Shelter Shelter;
    public int CurrentDay = 0;


    public Transform ShelterEntryPoint;
    public Transform SurfaceEntryPoint;

    public Image NextDayUI;

    private bool _isGameOver = false;

    public void Awake()
    {
        Instance = this;
        Init();
    }

    public void Update()
    {
        if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Init()
    {
        CurrentDay = 0;
        Player.Reset();
        Shelter.Reset();
    }

    public void NextDay()
    {
        StartCoroutine(NextDayCoroutine());
    }

    public void GameOver(GameoverReasone reasone)
    {
        Player.FpsController.playerCanMove = false;
        Player.FpsController.enableCameraMovement = false;
        
        NextDayUI.color = new Color(0,0,0,0);
        NextDayUI.gameObject.SetActive(true);
        NextDayUI.DOFade(1, 1f);

        var pos = Player.FpsController.playerCamera.localPosition.y;
        var rot = Player.FpsController.playerCamera.localEulerAngles;
        rot.z += 90;
        Player.FpsController.playerCamera.DOLocalMoveY(pos - 0.5f, 1f);
        Player.FpsController.playerCamera.DOLocalRotate(rot, 1f);
        
        string result = "YOU DIED \nfrom ";

        switch (reasone)
        {
            case GameoverReasone.Health:
                result += "poison.";
                break;
            case GameoverReasone.Food:
                result += "hunger.";
                break;
            case GameoverReasone.Water:
                result += "dehydration";
                break;
            default:
                result += "UNKNOWN REASON. HOW????";
                break;
        }

        result += "\n\n\n\n\n\n\n Press E to exit";
        
        MessageBox.Instance.ShowText(result,999);

        
        
        _isGameOver = true;

    }

    public void Victory()
    {
        string result = "YOU SURVIVE!\nThe military came and saved you.\n* Insert cutscene here *\nWell done!" +
                        "\n\n\n\n\n\nPree E to exit";
        
        MessageBox.Instance.ShowText(result,999);
        _isGameOver = true;
    }

    public enum GameoverReasone
    {
        Health,
        Food,
        Water
    }

    public void TeleportToSurface()
    {
        Player.transform.position = SurfaceEntryPoint.position;
        Player.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        Player.GoOutside();
    }

    public void TeleportToShelter()
    {
        Player.transform.position = ShelterEntryPoint.position;
        Player.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        Player.GoInside();
    }


    IEnumerator NextDayCoroutine()
    {
        Player.FpsController.enableCameraMovement = false;
        Player.FpsController.playerCanMove = false;
        Player.CanInteract = false;
        
        NextDayUI.color = new Color(0,0,0,0);
        NextDayUI.gameObject.SetActive(true);
        NextDayUI.DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
            
        CurrentDay++;

        if (CurrentDay >= 19)
        {
            Victory();
            yield break;
        }
        
        Shelter.NextDay();
        Player.NextDay();

        if (Player.Health <= 0)
        {
            GameOver(GameoverReasone.Health);
            yield break;
        }
        if (Player.Health <= 0)
        {
            GameOver(GameoverReasone.Food);
            yield break;
        }
        if (Player.Thirsty <= 0)
        {
            GameOver(GameoverReasone.Water);
            yield break;
        }

        
        
        MessageBox.Instance.ShowText("Day " + (CurrentDay+1));
        
        yield return new WaitForSeconds(2);
        
        NextDayUI.DOFade(0, 1f);
        
        
        yield return new WaitForSeconds(1f);
        
        
        
        Player.FpsController.enableCameraMovement = true;
        Player.FpsController.playerCanMove = true;
        Player.CanInteract = true;
    }
    
    
   
}