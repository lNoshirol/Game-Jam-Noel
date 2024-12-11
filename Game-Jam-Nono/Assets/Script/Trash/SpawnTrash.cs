using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    [SerializeField] GameObject trashSpawnManager;
    [SerializeField] List<GameObject> trashSpawnList;
    [SerializeField] GameObject trash;
    TrashPool trashPool;
    

    void Start()
    {
        trashSpawnList = new List<GameObject>();
        trashPool = GetComponent<TrashPool>();
        foreach (Transform child in trashSpawnManager.transform)
        {
            trashSpawnList.Add(child.gameObject);
        }

        foreach(GameObject child in trashSpawnList)
        {
            GameObject _trash = trashPool.GetTrash();
            _trash.transform.position = child.transform.position;
        }   
    }
}
