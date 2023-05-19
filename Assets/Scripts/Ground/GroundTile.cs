using UnityEngine;

public class GroundTile : MonoBehaviour
{
    // Game objects to be spawned on this tile
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject crouchObstaclePrefab;
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject enemyPrefab;

    // Positions where the objects can be spawned
    private readonly float[] spawnPositions = { -3f, 0, 3f };

    // Reference to the GroundSpawner and GameManager scripts
    private GroundSpawner groundSpawner;

    void Start()
    {
        // Find the GroundSpawner object in the scene
        groundSpawner = FindObjectOfType<GroundSpawner>();

        // Spawn obstacles and enemies if there are no more free tiles left
        if (GameManager.instance.tilesFreeFromObstacles <= 0)
        {
            // Choose whether to spawn an obstacle or an enemy
            if (Random.value > 0.5f)
            {
                SpawnObstacle();
            }
            else
            {
                SpawnEnemies();
            }
            // Reset the number of free tiles from obstacles
            GameManager.instance.tilesFreeFromObstacles = Random.Range(0, 2);
        }
        else
        {
            // Decrement the number of free tiles from obstacles
            GameManager.instance.tilesFreeFromObstacles--;

            int spawnIndex = Random.Range(0, 3);
            int numCoins = Random.Range(0, 3);
            SpawnCoins(spawnIndex, numCoins);
        }
    }


    // Called when the player exits the trigger collider on this tile
    private void OnTriggerExit(Collider other)
    {
        // Increase the player's score, spawn a new tile, and destroy this one
        GameManager.instance.IncrementScore(1);
        groundSpawner.SpawnTile();
        Destroy(gameObject, 5);
    }

    // Spawn an obstacle on this tile
    private void SpawnObstacle()
    {
        // Choose a spawn position for the obstacle and adjust its height depending on the type of obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        bool isCrouchObstacle = Random.value > 0.5f;
        float obstacleYPosition;
        if (!isCrouchObstacle)
        {
            obstacleYPosition = 0.5f;
            spawnPoint.position = new Vector3(0, obstacleYPosition, spawnPoint.position.z);

            // Spawn the obstacle and set its type
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            obstacle.GetComponent<Obstacle>().isCrouchObstacle = isCrouchObstacle;

            SpawnCoins(1, 2);
        }
        else
        {
            obstacleYPosition = 0f;
            spawnPoint.position = new Vector3(spawnPoint.position.x, obstacleYPosition, spawnPoint.position.z);

            // Spawn the obstacle and set its type
            GameObject obstacle = Instantiate(crouchObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            obstacle.GetComponent<Obstacle>().isCrouchObstacle = isCrouchObstacle;

            // spawn rocks in the other lanes, spawn three coins in the "binary" where the obstacle is in.
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

    // Spawn coins on this tile
    private void SpawnCoins(int spawnIndex, int numCoins)
    {
        // Spawn coins only if there is no crouching obstacle on this tile

        Vector3 coinSpawnPosition = new(spawnPositions[spawnIndex], 1f, 0);
        Instantiate(coinPrefab, transform.position + coinSpawnPosition, Quaternion.identity, transform);

        for (int i = 1; i < numCoins; i++)
        {
            // spawn other coins with a distance of 1.5f

            coinSpawnPosition += new Vector3(0, 0, 1.5f);
            Instantiate(coinPrefab, transform.position + coinSpawnPosition, Quaternion.identity, transform);
        }
    }



    // Spawn enemies on this tile
    private void SpawnEnemies()
    {
        // Choose a random number of enemies to spawn and a random position to spawn them
        int numEnemies = Random.Range(0, 3);
        int spawmIndex = Random.Range(0, 3);

        // Choose a spawn position for the enemy and spawn it
        Vector3 enemySpawnPosition = new(spawnPositions[spawmIndex], 0f, 5f);
        GameObject enemy = Instantiate(enemyPrefab, transform.position + enemySpawnPosition, Quaternion.Euler(0, 180, 0), transform);
        enemy.GetComponent<EnemyController>().health += GameManager.instance.score * 0.1f;

        Vector3 spawnPoint;
        int min = 2, max = 7;
        switch (spawmIndex)
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

    private void SpawnRock(Vector3 spawnPoint)
    {
        float obstacleYPosition = -0.96f;
        spawnPoint = new Vector3(spawnPoint.x, obstacleYPosition, spawnPoint.z);
        Instantiate(rockPrefab, spawnPoint, Quaternion.identity, transform);
    }
}
