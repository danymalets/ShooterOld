using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private float _deadZoneDistance = 1f;
    
    
    [SerializeField] private HealthBar _healthBar;
    
    [SerializeField] private int _startHealth = 100;
    [SerializeField] private int _health = 100;

    public UnityEvent Died;
    
    public Vector3 Position => transform.position;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }
    
    public void Initialize()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        
        _health = _startHealth;
        _healthBar.Initialize(_health);
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            
            Died?.Invoke();
        }
        
        _healthBar.UpdateValue(_health);
    }

    private void Update()
    {
        if (Position.y < _ground.position.y - _deadZoneDistance)
        {
            Died?.Invoke();
        }
    }
}
