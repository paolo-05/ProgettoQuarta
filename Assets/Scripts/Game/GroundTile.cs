using UnityEngine;

/// <summary>
/// Represents a ground tile in the game.
/// </summary>
public class GroundTile : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject crouchObstaclePrefab;
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject enemyPrefab;

    private readonly float[] spawnPositions = { -3f, 0, 3f };
    private GroundSpawner groundSpawner;

    private void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();

        if (GameManager.instance.tilesFreeFromObstacles <= 0)
        {
            if (Random.value > 0.5f)
            {
                SpawnObstacle();
            }
            else
            {
                SpawnEnemies();
            }

            GameManager.instance.tilesFreeFromObstacles = Random.Range(0, 2);
        }
        else
        {
            GameManager.instance.tilesFreeFromObstacles--;

            int spawnIndex = Random.Range(0, 3);
            int numCoins = Random.Range(0, 3);
            SpawnCoins(spawnIndex, numCoins);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.IncrementScore(1);
            groundSpawner.SpawnTile();
            Destroy(gameObject, 5);
        }
    }

    /// <summary>
    /// Spawns an obstacle on this ground tile.
    /// </summary>
    private void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        bool isCrouchObstacle = Random.value > 0.5f;
        float obstacleYPosition;

        if (!isCrouchObstacle)
        {
            obstacleYPosition = 0.5f;
            spawnPoint.position = new(0, obstacleYPosition, spawnPoint.position.z);

            GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            obstacle.GetComponent<Obstacle>().isCrouchObstacle = isCrouchObstacle;

            SpawnCoins(1, 2);
        }
        else
        {
            obstacleYPosition = 0f;
            spawnPoint.position = new(spawnPoint.position.x, obstacleYPosition, spawnPoint.position.z);

            GameObject obstacle = Instantiate(crouchObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            obstacle.GetComponent<Obstacle>().isCrouchObstacle = isCrouchObstacle;

            switch (obstacleSpawnIndex)
            {
                case 2:
                    spawnPoint = transform.GetChild(3).transform;
                    SpawnRock(spawnPoint.position);

                    spawnPoint = transform.GetChild(4).transform;
                    SpawnRock(spawnPoint.position);

                    SpawnCoins(0, 3);
                    break;
                case 3:
                    spawnPoint = transform.GetChild(2).transform;
                    SpawnRock(spawnPoint.position);

                    spawnPoint = transform.GetChild(4).transform;
                    SpawnRock(spawnPoint.position);

                    SpawnCoins(1, 3);
                    break;
                case 4:
                    spawnPoint = transform.GetChild(2).transform;
                    SpawnRock(spawnPoint.position);

                    spawnPoint = transform.GetChild(3).transform;
                    SpawnRock(spawnPoint.position);

                    SpawnCoins(2, 3);
                    break;
            }
        }
    }

    /// <summary>
    /// Spawns coins on this ground tile.
    /// </summary>
    private void SpawnCoins(int spawnIndex, int numCoins)
    {
        Vector3 coinSpawnPosition = new(spawnPositions[spawnIndex], 1f, 0);
        Instantiate(coinPrefab, transform.position + coinSpawnPosition, Quaternion.identity, transform);

        for (int i = 1; i < numCoins; i++)
        {
            coinSpawnPosition += new Vector3(0, 0, 1.5f);
            Instantiate(coinPrefab, transform.position + coinSpawnPosition, Quaternion.identity, transform);
        }
    }

    /// <summary>
    /// Spawns enemies on this ground tile.
    /// </summary>
    private void SpawnEnemies()
    {
        int spawnIndex = Random.Range(0, 3);

        Vector3 enemySpawnPosition = new(spawnPositions[spawnIndex], 0f, 5f);
        GameObject enemy = Instantiate(enemyPrefab, transform.position + enemySpawnPosition, Quaternion.Euler(0, 180, 0), transform);
        enemy.GetComponent<EnemyController>().health += GameManager.instance.score * 0.1f;

        Vector3 spawnPoint;
        int min = 2, max = 7;

        switch (spawnIndex)
        {
            case 0:
                if (Random.value > 0.5f)
                {
                    spawnPoint = transform.position + new Vector3(0, 0f, Random.Range(min, max));
                    SpawnRock(spawnPoint);
                }
                else
                {
                    spawnPoint = transform.position + new Vector3(3f, 0f, Random.Range(min, max));
                    SpawnRock(spawnPoint);
                }
                break;
            case 1:
                if (Random.value > 0.5f)
                {
                    spawnPoint = transform.position + new Vector3(-3f, 0, Random.Range(min, max));
                    SpawnRock(spawnPoint);
                }
                else
                {
                    spawnPoint = transform.position + new Vector3(3f, 0f, Random.Range(min, max));
                    SpawnRock(spawnPoint);
                }
                break;
            case 2:
                if (Random.value > 0.5f)
                {
                    spawnPoint = transform.position + new Vector3(-3f, 0f, Random.Range(min, max));
                    SpawnRock(spawnPoint);
                }
                else
                {
                    spawnPoint = transform.position + new Vector3(0f, 0f, Random.Range(min, max));
                    SpawnRock(spawnPoint);
                }
                break;
        }
    }

    /// <summary>
    /// Spawns a rock obstacle at the specified position.
    /// </summary>
    private void SpawnRock(Vector3 spawnPoint)
    {
        float obstacleYPosition = -0.96f;
        spawnPoint = new Vector3(spawnPoint.x, obstacleYPosition, spawnPoint.z);
        Instantiate(rockPrefab, spawnPoint, Quaternion.identity, transform);
    }
}
