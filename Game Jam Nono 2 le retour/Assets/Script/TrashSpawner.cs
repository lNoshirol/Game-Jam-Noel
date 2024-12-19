using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> trashObjects; // List of trash objects in the level (inactive at start)
    [SerializeField] private List<Transform> spawnPoints; // List of predefined spawn points for trash
    [SerializeField] private float spawnInterval = 20f; // Interval to spawn new trash
    private float spawnTimer;
    
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            // Call SpawnTrash when the timer hits zero
            SpawnTrash();

            // Reset the timer
            spawnTimer = spawnInterval;
        }
    }

    // Function to spawn a new trash item by teleporting it to a random spawn point
    private void SpawnTrash()
    {
        // Find an inactive trash object
        GameObject trashToActivate = GetInactiveTrashObject();

        if (trashToActivate != null)
        {
            // Select a random spawn point from the predefined list of spawn points
            Transform randomSpawnPoint = GetRandomSpawnPoint();

            // Teleport the inactive trash to the random spawn point and activate it
            trashToActivate.transform.position = randomSpawnPoint.position;
            trashToActivate.transform.rotation = randomSpawnPoint.rotation;
            trashToActivate.SetActive(true); // Activate the trash object

            Debug.Log("Spawned new trash at: " + randomSpawnPoint.position);
        }
        else
        {
            Debug.LogWarning("No inactive trash objects available to spawn.");
        }
    }

    // Get an inactive trash object from the list
    private GameObject GetInactiveTrashObject()
    {
        foreach (GameObject trash in trashObjects)
        {
            if (!trash.activeInHierarchy) // Check if the trash is inactive
            {
                return trash;
            }
        }
        return null; // No inactive trash found
    }

    // Get a random spawn point from the predefined list
    private Transform GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
}