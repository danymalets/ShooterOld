using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ZombieSpawner : MonoBehaviour, IZombieDestroyProvider
{
    [SerializeField] private ZombiePool _zombiePool;
    [SerializeField] private Player _player;
    
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _minDistanceToPlayer = 10;

    private int _level = 1;
    private int _zombies;
    private void Start()
    {
        SpawnZombie();
    }
    
    public void Destroy(Zombie zombie)
    {
        _zombiePool.Put(zombie);
        _zombies--;

        if (_zombies == 0)
        {
            _level++;
            for (int i = 0; i < _level; i++)
            {
                SpawnZombie();
            }
        }
    }

    private void SpawnZombie()
    {
        Vector3 zombiePosition;
        do
        {
            zombiePosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        } 
        while (Vector3.SqrMagnitude(_player.Position - zombiePosition) < _minDistanceToPlayer * _minDistanceToPlayer);
        
        Zombie zombie = _zombiePool.Get();
        zombie.transform.position = zombiePosition;
        zombie.Initialize(_player, this);

        _zombies++;
    }
}
