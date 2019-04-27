using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : InteractableItem
{
   public override void OnInteractFinished()
   {
       GameManager.Instance.TeleportToShelter();
     // MessageBox.Instance.ShowText("You did well!");
   }
}
