using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpItem : InteractableItem
{
    public InventoryItem Item;
    public bool IsToolboxNeeded;
    public bool AddItemToShelter;

    public override void Interact()
    {
        if (IsToolboxNeeded)
        {
            if (GameManager.Instance.Shelter.InventoryItems.FirstOrDefault(x => x.Name == "Toolbox") == null &&
                GameManager.Instance.Player.Inventory.FirstOrDefault(x => x.Name == "Toolbox") == null)
            {
                MessageBox.Instance.ShowText("You need toolbox for this");
                return;
            }
        }

        if (!AddItemToShelter && GameManager.Instance.Player.Inventory.Count >= 3)
        {
            MessageBox.Instance.ShowText("You can't hold more then 3 items");
            return;
        }

        base.Interact();
    }

    public override void OnInteractFinished()
    {
        if (AddItemToShelter)
        {
            GameManager.Instance.Shelter.AddItem(Item);
        }
        else
        {
            GameManager.Instance.Player.AddItem(Item);
        }
        Destroy(gameObject);
    }
}