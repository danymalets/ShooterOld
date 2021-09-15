using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private ZombieSpawner _zombieSpawner;
    [SerializeField] private Player _player;
    
    private void Start()
    {
        _player.Initialize();
        _zombieSpawner.StartSpawn();
    }
    
    public void OnPlayerDie()
    {
        _zombieSpawner.DestroyAllZombies();
        _player.Initialize();
        _zombieSpawner.StartSpawn();
    }
}