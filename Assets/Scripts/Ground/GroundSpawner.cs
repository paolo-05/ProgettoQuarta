// import of the necessay packages
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    // this script is used only at the game start, it generates the first 8 tiles.

    // groundTile is the game object that will be used to spawn new ground tiles.
    // nextSpawnPoint is the position of the next ground tile to be spawned.
    public GameObject groundTile;
    Vector3 nextSpawnPoint;

    // SpawnTile instantiates a new groundTile game object and sets nextSpawnPoint to the position of the next tile to be spawned.
    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }

    // Start function is called once at the start of the game.
    // It spawns the first 8 ground tiles by calling the SpawnTile function 8 times in a loop.
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnTile();
        }
    }
}
