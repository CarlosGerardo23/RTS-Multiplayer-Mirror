using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerRTS : NetworkManager
{
    [SerializeField] private GameObject _unitSpawnerPrefab;
    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("Connected to the server");
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        GameObject unitSpawnerInstance = Instantiate(_unitSpawnerPrefab, conn.identity.transform.position, conn.identity.transform.rotation);
        NetworkServer.Spawn(unitSpawnerInstance,conn);
    }
}
