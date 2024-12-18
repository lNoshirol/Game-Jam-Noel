using FishNet.Transporting.Tugboat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arg : MonoBehaviour
{
    public GameObject networkManager;

    public void UpdateIpAdressInTUBGOAT()
    {
        networkManager.GetComponent<Tugboat>().SetClientAddress(GetComponent<TMP_InputField>().text);
    }
}
