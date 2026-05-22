using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage, GameObject attacker, Vector3 hitPosition);
}

