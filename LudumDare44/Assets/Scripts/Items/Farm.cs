using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Modifiers:     Temperature>50 (+1)
//               Light (+1)

public class Farm : InteractableItem
{
    public const int DAYS_TO_GENERATE_FOOD = 3;
    public int DaysLeft;

    public bool CanCollect = false;

    public InventoryItem Item;

    public void Awake()
    {
        DaysLeft = DAYS_TO_GENERATE_FOOD;
        IsBroken = true;
    }

    public override void NextDay()
    {
        int modifier = 0;
        modifier += GameManager.Instance.Shelter.Lamp.IsOn ? 1 : 0;
        modifier += GameManager.Instance.Shelter.Temperature > 50 ? 1 : 0;

        if (CanCollect) return;


        DaysLeft--;
        if (DaysLeft - modifier <= 0)
        {
            CanCollect = true;
        }
    }

    public override void Interact()
    {
        if (IsBroken)
        {
            base.Interact();

            return;
        }


        if (CanCollect)
        {
            base.Interact();
        }
        else
        {
            int modifier = 0;
            modifier += GameManager.Instance.Shelter.Lamp.IsOn ? 1 : 0;
            modifier += GameManager.Instance.Shelter.Temperature > 50 ? 1 : 0;
            int res = DaysLeft - modifier;

            MessageBox.Instance.ShowText("You need to wait " + res + " more day" + (res > 1 ? "s" : ""));
        }
    }

    public override void OnInteractFinished()
    {
        GameManager.Instance.Shelter.AddItem(Item);
        CanCollect = false;
        DaysLeft = DAYS_TO_GENERATE_FOOD;

        base.OnInteractFinished();
    }
}