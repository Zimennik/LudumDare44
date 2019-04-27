using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public Player Player;
   public Shelter Shelter;

   public int CurrentDay = 0;

   public void Awake()
   {
      Instance = this;
      Init();
   }
   
   public void Init()
   {
      CurrentDay = 0;
      Player.Reset();
      Shelter.Reset();

      StartCoroutine(HealthDecrease());
   }

   public void NextDay()
   {
      
   }

   public void GameOver()
   {
      
   }

   IEnumerator HealthDecrease()
   {
      while (Player.Health>=0)
      {
         Player.Health--;
         yield return new WaitForSeconds(1);
      }
   }
   
}
