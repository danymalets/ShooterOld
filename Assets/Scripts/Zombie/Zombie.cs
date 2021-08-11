using System;
using System.Collections;
using DG.Tweening;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Zombie : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar _healthBar;
    
    [SerializeField] private Blood _blood;
    [SerializeField] private ParticleSystem _appearance;
    [SerializeField] private ParticleSystem _disappearance;
    
    [SerializeField] private GameObject _meshes;

    [SerializeField] private int _startHealth = 100;
    [SerializeField] private int _health;

    private Ragdoll _ragdoll;
    private ZombieMover _mover;
    private IZombieDestroyProvider _zombieDestroyProvider;
    
    private void Awake()
    {
        _ragdoll = GetComponent<Ragdoll>();
        _mover = GetComponent<ZombieMover>();
        _healthBar.Initialize(_startHealth);
    }
    
    public void Initialize(Player target, IZombieDestroyProvider zombieDestroyProvider)
    {
        _zombieDestroyProvider = zombieDestroyProvider;
        _mover.SetTarget(target);
        _health = _startHealth;
        _ragdoll.MakeAnimated();
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        _meshes.SetActive(false);
        _appearance.Play();
        yield return new WaitForSeconds(0.1f);
        _meshes.SetActive(true);
        _mover.StartFollow();
    }
    
    public void AcceptDamage(Vector3 hitPosition, Vector3 hitNormal, int damage)
    {
        if (damage < 0)
            throw new ArgumentException();

        if (_health <= 0)
            return;
        
        _health -= damage;

        _blood.Spew(hitPosition, hitNormal);

        _healthBar.UpdateValue(_health);

        if (_health <= 0)
        {
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear()
    {
        _healthBar.Destroy();
        _ragdoll.MakePhysical();
        _mover.StopFollowing();
        
        yield return new WaitForSeconds(3);

        _disappearance.Play();

        yield return new WaitForSeconds(0.3f);
        
        _meshes.SetActive(false);
        
        yield return new WaitForSeconds(1);

        _meshes.SetActive(true);
        
        _zombieDestroyProvider.Destroy(this);
    }
}
