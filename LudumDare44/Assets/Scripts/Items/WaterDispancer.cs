using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispancer : InteractableItem
{
    public const int DAYS_TO_GENERATE_WATER = 3;
    public const int ENERGY_CONSUMPTION = 11;
    public int DaysLeft;

    public bool CanCollect = false;

    public InventoryItem Item;

    public void Awake()
    {
        DaysLeft = DAYS_TO_GENERATE_WATER;
    }

    public override void NextDay()
    {
        if (CanCollect) return;

        if (GameManager.Instance.Shelter.Energy >= ENERGY_CONSUMPTION)
        {
            GameManager.Instance.Shelter.Energy -= ENERGY_CONSUMPTION;
            DaysLeft--;
            if (DaysLeft == 0)
            {
                CanCollect = true;
            }
        }
    }

    public override void Interact()
    {
        if (CanCollect)
        {
            base.Interact();
        }
        else
        {
            MessageBox.Instance.ShowText("You need to wait " + DaysLeft + " more day" + (DaysLeft > 1 ? "s" : ""));
        }


        
    }

    public override void OnInteractFinished()
    {
        GameManager.Instance.Shelter.AddItem(Item);
        CanCollect = false;
        DaysLeft = DAYS_TO_GENERATE_WATER;
        base.OnInteractFinished();
    }
}