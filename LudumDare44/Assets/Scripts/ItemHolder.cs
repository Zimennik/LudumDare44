using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Transform Holder;

    public void Awake()
    {
        foreach (Transform t in Holder)
                {
                    t.gameObject.SetActive(false);
                }
    }
    
    public void SetCount(int count)
    {
        int i = 0;
        foreach (Transform t in Holder)
        {
            t.gameObject.SetActive(i < count);
            i++;
        }
    }
}
