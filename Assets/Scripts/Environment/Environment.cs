using UnityEngine;

public class Environment : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _bulletMark;

    public void AcceptDamage(Vector3 hitPosition, Vector3 hitNormal, int damage)
    {
        Instantiate(_bulletMark, hitPosition + hitNormal * 0.01f, Quaternion.LookRotation(-hitNormal));
    }
}
