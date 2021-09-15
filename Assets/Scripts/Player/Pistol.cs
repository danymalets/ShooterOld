using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private GameInput _input;
    
    [SerializeField] private ParticleSystem _shoot;

    [SerializeField] private int _damage = 30;
    [SerializeField] private float _shootCooldown = 0.2f;
    [SerializeField] private float _maxDistance = 100f;

    private float _waitTime;
    
    private void Update()
    {
        if (_input.Shoot && _waitTime < 0f)
        {
            Shoot();
            _waitTime = _shootCooldown;
        }
        else
        {
            _waitTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        _shoot.Play();
        Debug.Log(1);
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _maxDistance, _hitMask))
        {
            GameObject target = hit.collider.transform.root.gameObject;

            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.AcceptDamage(hit.point, hit.normal, _damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxDistance);
    }
}
