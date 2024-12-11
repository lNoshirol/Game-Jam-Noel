using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignPlayer : MonoBehaviour
{
    [SerializeField] Camera cam;
    public int playerID;

    [SerializeField] TextMeshProUGUI _playerNameText;

    void Start()
    {
        PlayerManager.instance.AddPlayer(gameObject);
    }

    public void Assign()
    {
        _playerNameText.text = gameObject.name;
    }
}
