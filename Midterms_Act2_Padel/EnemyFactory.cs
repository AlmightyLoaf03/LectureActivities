using UnityEngine;

public class EnemyFactory : Factory
{
    [SerializeField] private GameObject enemyPrefab; // assign prefab in Inspector

    public override IProduct GetProduct(Vector3 position)
    {
        // Instantiate prefab
        GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);

        // Return its IProduct interface
        return enemyObj.GetComponent<IProduct>();
    }
}

