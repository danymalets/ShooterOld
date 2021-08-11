using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    [SerializeField] private int _originalSize = 30;
    
    [SerializeField] private Zombie _zombiePrefab;

    private Stack<Zombie> _pool = new Stack<Zombie>();
    private void Awake()
    {
        for (int i = 0; i < _originalSize; i++)
        {
            Zombie zombie = Instantiate(_zombiePrefab);
            zombie.gameObject.SetActive(false);
            _pool.Push(zombie);
        }
    }

    public Zombie Get()
    {
        if (_pool.Count == 0)
        {
            Debug.LogWarning("Zombie pool so small!");
            Zombie zombie = Instantiate(_zombiePrefab);
            zombie.gameObject.SetActive(false);
            return zombie;
        }
        else
        {
            Zombie zombie = _pool.Pop();
            zombie.gameObject.SetActive(true);
            return zombie;
        }
    }

    public void Put(Zombie zombie)
    {
        zombie.gameObject.SetActive(false);
        _pool.Push(zombie);
    }
}