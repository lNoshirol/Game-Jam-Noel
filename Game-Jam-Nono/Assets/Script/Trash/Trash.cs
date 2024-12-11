using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trash : MonoBehaviour
{
    Transform trashStock;
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        trashStock = collision.gameObject.transform.Find("TrashPosition");
        GetTrash(trashStock);
    }

    private void GetTrash(Transform trashStock)
    {
        gameObject.transform.parent = trashStock;
        gameObject.transform.position = trashStock.position;
    }
}
