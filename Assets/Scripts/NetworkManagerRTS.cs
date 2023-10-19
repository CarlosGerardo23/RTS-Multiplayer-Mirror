using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerRTS : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("Connected to the server");
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log($"Server add player, current player numbers on server {numPlayers}");
    }
}
