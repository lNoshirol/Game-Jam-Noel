using System.Collections.Generic;
using UnityEngine;

public class TrashObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject trashPrefab; // The trash object prefab
    public int initialPoolSize = 10; // Initial number of trash objects in the pool
    public Transform[] spawnPoints; // List of spawn points for the trash

    [SerializeField] private List<GameObject> trashPool = new List<GameObject>(); // List for pooling the trash objects
    [SerializeField] private List<GameObject> activeTrash = new List<GameObject>(); // Active trash objects in the game

    private void Start()
    {
        InitializePool();
    }

    // Initialize the pool by creating trash objects and storing them in the pool
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject trash = Instantiate(trashPrefab);
            trash.SetActive(false); // Disable it initially
            trashPool.Add(trash);
        }
    }

    // Spawn trash when needed
    public void SpawnTrash()
    {
        GameObject trash = GetInactiveTrash();
        Debug.Log("Hello");
        if (trash != null)
        {
            
            trash.SetActive(true); // Activate it

            // Get a random spawn point from the array of spawn points
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            trash.transform.position = spawnPoint.position;

            // Track this active trash
            activeTrash.Add(trash);
        }
        else
        {
            Debug.LogWarning("No more trash in pool! Consider expanding the pool size.");
        }
    }

    // Get an inactive trash object from the pool
    private GameObject GetInactiveTrash()
    {
        // Look through the pool for an inactive trash object
        foreach (var trash in trashPool)
        {
            if (!trash.activeInHierarchy)
            {
                return trash;
            }
        }

        return null; // No inactive objects available
    }

    // Return trash back to the pool when it is collected (or no longer needed)
    public void ReturnTrashToPool(GameObject trash)
    {
        trash.SetActive(false); // Deactivate the trash
        activeTrash.Remove(trash); // Remove it from active list
    }

    // Check if all trash objects are collected (not active)
    public bool AreAllTrashCollected()
    {
        return activeTrash.Count == 0;
    }
}
