using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _target;

    private Renderer _renderer;

    void Awake()
    {
        _target = Camera.main.transform;
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_renderer != null)
        {
            _renderer.enabled = Vector3.Distance(_target.position, transform.position) < 10;
        }
        transform.LookAt(_target);
    }
}