using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class prout : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject snowParent;

    public GameObject last;
    public Vector3 decalage;



    [Button("DUPLIQUE")]
    public void Duplication()
    {
        GameObject newPlane = Instantiate(snowPrefab);
        newPlane.transform.SetParent(snowParent.transform);
        newPlane.transform.position = last.transform.position + decalage;
        last = newPlane;
    }
}
