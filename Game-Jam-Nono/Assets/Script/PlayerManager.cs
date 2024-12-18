using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] List<GameObject> _playersList;

    private void Awake()
    {
        instance = this;
    }

    public void AddPlayer(GameObject player)
    {
        player.GetComponent<AssignPlayer>().playerID = _playersList.Count;
        player.name = "Player " + (_playersList.Count+1);
        _playersList.Add(player);
        player.GetComponent<AssignPlayer>().Assign();
    }
}
