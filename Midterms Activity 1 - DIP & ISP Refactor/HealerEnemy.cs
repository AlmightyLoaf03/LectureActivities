using UnityEngine;

public class HealerEnemy : IMovable, IAttackable, IHealable
{
    public void Move()
    {
        Debug.Log("Healer moves!");
    }

    public void Attack()
    {
        Debug.Log("Healer attacks!");
    }

    public void Heal()
    {
        Debug.Log("Healer heals nearby ally!");
    }
}
