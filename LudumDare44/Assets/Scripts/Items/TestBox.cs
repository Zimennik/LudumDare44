using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : InteractableItem
{
   
   public override void OnInteractFinished()
   {
      print("You kill this box...");
      Destroy(gameObject);
   }
   
   
}
