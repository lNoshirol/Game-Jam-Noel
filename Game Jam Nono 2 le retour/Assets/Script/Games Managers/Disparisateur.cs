using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparisateur : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        StartCoroutine(Disparait());
    }

    IEnumerator Disparait()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
