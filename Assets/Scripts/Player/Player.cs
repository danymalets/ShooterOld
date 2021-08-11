using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public Vector3 Position => transform.position;

    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (damage <= 0)
        {
            Destroy(gameObject);
        }
    }
}
