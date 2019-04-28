using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
      source.DOFade(1, 1f);
   }

   public void Stop()
   {
      source.DOFade(0, 1f).OnComplete(() => { source.Stop(); });
   }


}
