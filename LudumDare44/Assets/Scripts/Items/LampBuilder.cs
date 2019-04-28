using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LampBuilder : InteractableItem
{
    public override void Interact()
    {
        var lightbulb = GameManager.Instance.Shelter.InventoryItems.FirstOrDefault(x => x.Name == "Lightbulb");
        if(lightbulb==null)return;
        
        base.Interact();
    }

    public override void OnInteractFinished()
    {
        GameManager.Instance.Shelter.BuildLamp();
        base.OnInteractFinished();
    }
}
