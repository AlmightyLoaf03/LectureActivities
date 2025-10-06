using UnityEngine;

public class Goblin : IMovable, IAttackable
{
    public void Move()
    {
        Debug.Log("Goblin moves!");
    }
    public void Attack()
    {
        Debug.Log("Goblin attacks!");
    }
}
