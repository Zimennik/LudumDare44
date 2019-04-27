using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : InteractableItem
{
   public override void OnInteractFinished()
   {
      MessageBox.Instance.ShowText("You did well!");
   }
}
