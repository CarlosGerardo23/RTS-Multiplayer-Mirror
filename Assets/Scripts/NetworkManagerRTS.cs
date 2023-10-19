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
        PlayerNetwork player= conn.identity.GetComponent<PlayerNetwork>();
        player.SetPlayerName($"Player: {numPlayers}");
        player.SetColor(new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),255f));
    }
}
