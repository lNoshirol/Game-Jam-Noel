using FishNet.Object;
using UnityEngine;

public class GameController : NetworkBehaviour
{
    public TrashObjectPool trashPool; // Reference to the trash pool
    public float spawnInterval = 20f; // Time interval to spawn new trash (20 seconds)
    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval; // Set initial timer to the spawn interval
    }

    private void Update()
    {
        // Decrease the timer
        spawnTimer -= Time.deltaTime;

        // Check if the timer has reached 0 (or below), meaning it's time to spawn trash
        if (spawnTimer <= 0f)
        {
            // Spawn new trash
            trashPool.SpawnTrash();

            // Reset the timer to the spawn interval
            spawnTimer = spawnInterval;
        }

        // Check if all trash is collected and spawn if necessary
        if (trashPool.AreAllTrashCollected())
        {
            trashPool.SpawnTrash();
        }
    }
}
