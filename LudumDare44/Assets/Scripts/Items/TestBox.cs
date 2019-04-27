using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : InteractableItem
{
   
   public override void OnInteractFinished()
   {
      MessageBox.Instance.ShowText("You kill this box...");
      Destroy(gameObject);
   }
   
   
}
