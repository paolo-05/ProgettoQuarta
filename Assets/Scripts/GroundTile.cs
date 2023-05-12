using UnityEngine;

public class GroundTile : MonoBehaviour
{
    // Game objects to be spawned on this tile
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject crouchObstaclePrefab;
    [SerializeField] GameObject enemyPrefab;

    // Positions where the objects can be spawned
    private readonly float[] spawnPositions = { -3f, 0, 3f };

    // Reference to the GroundSpawner and GameManager scripts
    GroundSpawner groundSpawner;
    GameManager gameManager;

    void Start()
    {
        // Find the GroundSpawner and GameManager objects in the scene
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        // Decrement the number of tiles free from obstacles
        if (gameManager.tilesFreeFromObstacles > 0)
        {
            gameManager.tilesFreeFromObstacles--;
        }
        // Spawn obstacles and enemies if there are no more free tiles left
        if (gameManager.tilesFreeFromObstacles <= 0)
        {
            SpawnObstacle();
            SpawnEnemies();
        }
        // Spawn coins on this tile
        SpawnCoins();
    }

    // Called when the player exits the trigger collider on this tile
    private void OnTriggerExit(Collider other)
    {
        // Increase the player's score, spawn a new tile, and destroy this one
        gameManager.IncrementScore(1);
        groundSpawner.SpawnTile();
        Destroy(gameObject, 5);
    }

    // Spawn an obstacle on this tile
    void SpawnObstacle()
    {
        // Choose a spawn position for the obstacle and adjust its height depending on the type of obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        bool isCrouchObstacle = Random.value > 0.5f;
        float obstacleYPosition;
        if (!isCrouchObstacle)
        {
            obstacleYPosition = 0.8f;
            spawnPoint.position = new Vector3(0, obstacleYPosition, spawnPoint.position.z);

            // Spawn the obstacle and set its type
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            return;
        }
        else
        {
            obstacleYPosition = 2f;
            spawnPoint.position = new Vector3(spawnPoint.position.x, obstacleYPosition, spawnPoint.position.z);

            // Spawn the obstacle and set its type
            GameObject obstacle = Instantiate(crouchObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            obstacle.GetComponent<Obstacle>().isCrouchObstacle = isCrouchObstacle;
        }


    }

    // Spawn coins on this tile
    void SpawnCoins()
    {
        // Choose a random number of coins to spawn and a random position to spawn them
        int numCoins = Random.Range(0, 3);
        int spawnIndex = Random.Range(0, 3);
        for (int i = 0; i < numCoins; i++)
        {
            Vector3 coinSpawnPosition = new(spawnPositions[spawnIndex], 1f, Random.Range(-3f, 3f));
            GameObject coin = Instantiate(coinPrefab, transform.position + coinSpawnPosition, Quaternion.identity, transform);
        }
    }

    // Spawn enemies on this tile
    void SpawnEnemies()
    {
        // Choose a random number of enemies to spawn and a random position to spawn them
        int numEnemies = Random.Range(0, 3);
        int spawmIndex = Random.Range(0, 3);

        // Choose a spawn position for the enemy and spawn it
        Vector3 enemySpawnPosition = new(spawnPositions[spawmIndex], 0f, Random.Range(-3, 3f));
        GameObject enemy = Instantiate(enemyPrefab, transform.position + enemySpawnPosition, Quaternion.Euler(0, 180, 0), transform);
    }
}
