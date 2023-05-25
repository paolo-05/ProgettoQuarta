using UnityEngine;

/// <summary>
/// Spawns and manages the initial ground tiles at the start of the game.
/// </summary>
public class GroundSpawner : MonoBehaviour
{
    /// <summary>
    /// The prefab for the ground tile to be spawned.
    /// </summary>
    public GameObject groundTile;

    /// <summary>
    /// The next point for spawining a new tile.
    /// </summary>
    private Vector3 nextSpawnPoint;

    /// <summary>
    /// Spawns a new ground tile and updates the next spawn point.
    /// </summary>
    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }

    /// <summary>
    /// Initializes the ground spawner by spawning the initial ground tiles.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnTile();
        }
    }
}
