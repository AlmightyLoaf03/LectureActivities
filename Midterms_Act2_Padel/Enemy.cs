using UnityEngine;

public class Enemy : MonoBehaviour, IProduct
{
    public void Spawn(Vector3 position)
    {
        transform.position = position;
        Debug.Log("Enemy spawned at " + position);
    }
}
