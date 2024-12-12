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
        if (collision.gameObject.GetComponent<PlayerController>().grabTrash) { Debug.Log("HALLOOO"); return; }
        trashStock = collision.gameObject.transform.Find("TrashStock");
        collision.gameObject.GetComponent<PlayerController>().grabTrash = true;
        GetTrash(trashStock);

    }

    private void GetTrash(Transform trashStock)
    {
        gameObject.transform.parent = trashStock;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        gameObject.transform.position = trashStock.position;
        gameObject.tag = trashStock.tag;
    }
}