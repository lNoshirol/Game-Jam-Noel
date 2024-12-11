using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPool : MonoBehaviour
{
    private List<GameObject> trashPool = new();
    public int trashPoolSize;
    public GameObject trashPrefab;

    public static TrashPool Instance { get; private set; }  // Instance Singleton

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        for (int i = 0; i < trashPoolSize; i++)
        {
            GameObject trash = Instantiate(trashPrefab);
            trash.SetActive(false);
            trashPool.Add(trash);
        }
    }
    public GameObject GetTrash()
    {
        Debug.Log(trashPool);
        foreach (GameObject _trash in trashPool)
        {
            if (!_trash.activeInHierarchy)
            {
                _trash.SetActive(true);
                return _trash;
            }
        }
        return GetNewTrash();
    }

    GameObject GetNewTrash()
    {
        GameObject newProjectile = Instantiate(trashPrefab);
        newProjectile.SetActive(true);
        trashPool.Add(newProjectile);
        return newProjectile;
    }

    public void RemoveTrash(GameObject trash)
    {
        trash.SetActive(false);
    }



}
