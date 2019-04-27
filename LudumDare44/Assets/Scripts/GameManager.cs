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

        StartCoroutine(HealthDecrease());
    }

    public void NextDay()
    {
    }

    public void GameOver()
    {
    }

    public void TeleportToSurface()
    {
        Player.transform.position = SurfaceEntryPoint.position;
        Player.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
    }

    public void TeleportToShelter()
    {
        Player.transform.position = ShelterEntryPoint.position;
        Player.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        //Player.FpsController.
    }

    IEnumerator HealthDecrease()
    {
        while (Player.Health >= 0)
        {
            Player.Health--;
            yield return new WaitForSeconds(1);
        }
    }
}