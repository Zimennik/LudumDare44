using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
   public static MusicManager Instance;

   public AudioSource source;
   
   public void Awake()
   {
      Instance = this;
   }

   public void Play()
   {
      source.Play();
   }

   public void Stop()
   {
      source.Stop();
   }


}
