using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodShelf : InteractableItem
{
    public override void Interact()
    {
        if (GameManager.Instance.Shelter.InventoryItems.Count(x => x.Name == "Food") > 0)
        {
            base.Interact();
        }
        else
        {
            MessageBox.Instance.ShowText("You don't have any food");
        }
    }

    public override void OnInteractFinished()
    {
        var food = GameManager.Instance.Shelter.InventoryItems.FirstOrDefault(x => x.Name == "Food");
        if (food == null) return;

        GameManager.Instance.Shelter.RemoveItem(food);
        
        GameManager.Instance.Player.Hungry += 100;
        GameManager.Instance.Player.Health += 5;
    }
}