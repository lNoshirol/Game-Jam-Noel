using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trash : MonoBehaviour
{
    Transform trashStock;
    public GameObject lastPlayer;
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null) { Debug.Log("Y A RIEEEEEEEEEEN"); return; }
        if (collision.gameObject.GetComponent<PlayerController>().grabTrash) { Debug.Log("HALLOOO"); return; }
        trashStock = collision.gameObject.GetComponent<PlayerController>().trashStock.transform;
        collision.gameObject.GetComponent<PlayerController>().grabTrash = true;
        lastPlayer = collision.gameObject;
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