using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkRTSPlayer : NetworkBehaviour
{
  [SerializeField]  private List<UnitNetworkController> _myUnits = new List<UnitNetworkController>();
    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();
        UnitNetworkController.OnUnitSpawnedServerEvent += ServerHanldeUnitSpawned;
        UnitNetworkController.OnUnitDespawnedServerEvent += ServerHandleDespawned;
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        UnitNetworkController.OnUnitSpawnedServerEvent -= ServerHanldeUnitSpawned;
        UnitNetworkController.OnUnitDespawnedServerEvent -= ServerHandleDespawned;
    }

    private void ServerHanldeUnitSpawned(UnitNetworkController unit)
    {
        if (unit.connectionToClient.connectionId == connectionToClient.connectionId)
            _myUnits.Add(unit);

    }
    private void ServerHandleDespawned(UnitNetworkController unit)
    {
        if (unit.connectionToClient.connectionId == connectionToClient.connectionId)
            _myUnits.Remove(unit);
    }
    #endregion
    #region Client
    public override void OnStartClient()
    {
        if (isClientOnly)
        {
            UnitNetworkController.OnUnitSpawnedClientEvent += ClientHanldeUnitSpawned;
            UnitNetworkController.OnUnitDespawnedClientEvent += ClientHandleDespawned;
        }


    }
    public override void OnStopClient()
    {
        if (isClientOnly)
        {
            UnitNetworkController.OnUnitSpawnedClientEvent -= ClientHanldeUnitSpawned;
            UnitNetworkController.OnUnitDespawnedClientEvent -= ClientHandleDespawned;
        }


    }
    private void ClientHanldeUnitSpawned(UnitNetworkController unit)
    {
        if (isOwned)
            _myUnits.Add(unit);

    }
    private void ClientHandleDespawned(UnitNetworkController unit)
    {
        if (isOwned)
            _myUnits.Remove(unit);
    }
    #endregion
}
