using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    
    public void Spew(Vector3 hitPosition, Vector3 hitNormal)
    {
        transform.position = hitPosition;
        transform.LookAt(-hitNormal);
        _particleSystem.Play();
    }
}