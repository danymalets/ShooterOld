using UnityEngine;

public interface IDamageable
{
    public void AcceptDamage(Vector3 hitPosition, Vector3 hitNormal, int damage);
}
