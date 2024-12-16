using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{

    private static PlayerManager instance;

    private List<Player> _player = new List<Player>();
    // Start is called before the first frame update

    public static void InitializeNewPlayer(int clientID)
    {
        //instance._players.Add(clientID, new Player());
    }

    public static void PlayerGain(int clientID)
    {
        
    }

    class Player
    {
        public int ClientID = -1;
        public int Score = 0;
    }
}
