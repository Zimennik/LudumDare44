using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WaterShelf : InteractableItem
{
    public override void Interact()
    {
        if (GameManager.Instance.Shelter.InventoryItems.Count(x => x.Name == "Water") > 0)
        {
            base.Interact();
        }
        else
        {
            MessageBox.Instance.ShowText("You don't have any Water");
        }
    }

    public override void OnInteractFinished()
    {
        var water = GameManager.Instance.Shelter.InventoryItems.FirstOrDefault(x => x.Name == "Water");
        if (water == null) return;

        GameManager.Instance.Shelter.RemoveItem(water);

        GameManager.Instance.Player.Thirsty += 100;
    }
}