using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public GameObject coinPrefab;
    GroundSpawner groundSpawner;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if (gameManager.tilesFreeFromObstacles > 0 )
        {
            gameManager.tilesFreeFromObstacles--;
        }
        if (gameManager.tilesFreeFromObstacles <= 0)
        {
            SpawnObstacle();
        }
        SpawnCoins();
    }

    private void OnTriggerExit(Collider other) 
    {
        GameManager.instance.IncrementScore();
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject obstaclePrefab;

    void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        bool isCrouchObstacle = Random.value > 0.5f; // 50% chance to spawn a crouch obstacle
        float obstacleYPosition = isCrouchObstacle ? 2f : 0.5f;
        spawnPoint.position = new Vector3(spawnPoint.position.x, obstacleYPosition, spawnPoint.position.z);

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        obstacle.GetComponent<Obstacle>().isCrouchObstacle = isCrouchObstacle;
    }

    void SpawnCoins()
    {
        int numCoins = Random.Range(0, 3);
        for (int i = 0; i < numCoins; i++)
        {
            Vector3 coinSpawnPosition = new Vector3(Random.Range(-3, 3f), 1f, Random.Range(-3f, 3f));
            GameObject coin = Instantiate(coinPrefab, transform.position + coinSpawnPosition, Quaternion.Euler(0, 90, 0), transform);
        }
    }

}
