using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pick a random spawn position
            Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            // Use the factory to spawn an enemy
            IProduct enemy = enemyFactory.GetProduct(randomPos);
            enemy.Spawn(randomPos);
        }
    }
}
