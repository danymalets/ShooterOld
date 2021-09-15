using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ZombieSpawner : MonoBehaviour, IZombieDestroyProvider
{
    [SerializeField] private Notifications _notifications;
    
    [SerializeField] private ZombiePool _zombiePool;
    [SerializeField] private Player _player;
    
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _minDistanceToPlayer = 10;

    private int _wave;
    private int _deadZombies;
    private List<Zombie> _liveZombies = new List<Zombie>();
    
    public void StartSpawn()
    {
        _wave = 1;
        SpawnZombies();
    }
    
    public void Destroy(Zombie zombie)
    {
        _zombiePool.Put(zombie);
        _deadZombies--;

        if (_deadZombies == 0)
        {
            _wave++;
            SpawnZombies();
        }
    }

    private void SpawnZombies()
    {
        _notifications.NotifyAboutWave(_wave);
        for (int i = 0; i < (1 <<_wave); i++)
        {
            SpawnZombie();
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
        _liveZombies.Add(zombie);
        _deadZombies++;
    }

    public void DestroyAllZombies()
    {
        foreach (Zombie zombie in _liveZombies)
        {
            _zombiePool.Put(zombie);
        }
    }
}

public interface IZombieDestroyProvider
{
    public void Destroy(Zombie zombie);
}