using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public Player Player;
   public Shelter Shelter;
   

   public void Awake()
   {
      Instance = this;
   }
   
   public void Init()
   {
      
   }

   public void GameOver()
   {
      
   }
   
}
