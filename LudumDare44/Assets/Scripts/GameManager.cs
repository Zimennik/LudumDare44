using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player Player;
    public Shelter Shelter;
    public int CurrentDay = 0;


    public Transform ShelterEntryPoint;
    public Transform SurfaceEntryPoint;

    public void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {
        CurrentDay = 0;
        Player.Reset();
        Shelter.Reset();

        
    }

    public void NextDay()
    {
        Shelter.NextDay();
        Player.NextDay();
    }

    public void GameOver()
    {
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

   
}