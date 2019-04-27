using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _target;

    void Awake()
    {
        _target = Camera.main.transform;
    }
    
    void Update()
    {
        transform.LookAt(_target);
    }
}
