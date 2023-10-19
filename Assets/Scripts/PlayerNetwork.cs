using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private string _playerName = "Missing Name";

    [Server]
    public void SetPlayerName(string playerName)
    {
        _playerName = playerName;
    }
}
