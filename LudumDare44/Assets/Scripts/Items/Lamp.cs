using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : InteractableItem
{
    public GameObject Light;
    public const int ENERGY_CONSUMPTION = 11;

    public bool IsOn;


    public void Awake()
    {
        Light.SetActive(true);
        
    }
    
    public override void NextDay()
    {
        base.NextDay();
        if (GameManager.Instance.Shelter.Energy >= ENERGY_CONSUMPTION)
        {
            GameManager.Instance.Shelter.Energy -= ENERGY_CONSUMPTION;
            SetActive(true);
        }
        else
        {
           SetActive(false);
        }
    }

    public void SetActive(bool b)
    {
        Light.SetActive(b);
        IsOn = b;
    }
}
