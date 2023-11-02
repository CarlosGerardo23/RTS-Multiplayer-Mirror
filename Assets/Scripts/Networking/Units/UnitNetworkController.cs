using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class UnitNetworkController : NetworkBehaviour
{
    [SerializeField] private UnityEvent _onSelected = null;
    [SerializeField] private UnityEvent _onDeselect = null;

    public static event Action<UnitNetworkController> OnUnitSpawnedServerEvent;
    public static event Action<UnitNetworkController> OnUnitDespawnedServerEvent;
    public static event Action<UnitNetworkController> OnUnitSpawnedClientEvent;
    public static event Action<UnitNetworkController> OnUnitDespawnedClientEvent;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();
        OnUnitSpawnedServerEvent?.Invoke(this);

    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        OnUnitDespawnedServerEvent?.Invoke(this);

    }
    #endregion
    #region Client
    [Client]
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isClientOnly && isOwned)
            OnUnitSpawnedClientEvent?.Invoke(this);
    }
    [Client]
    public override void OnStopClient()
    {
        base.OnStopClient();
        if (isClientOnly && isOwned)
            OnUnitDespawnedClientEvent?.Invoke(this);
    }
    [Client]
    public void Select()
    {
        if (isOwned)
            _onSelected?.Invoke();
    }
    [Client]
    public void Deselect()
    {
        if (isOwned)
            _onDeselect?.Invoke();
    }
    #endregion
}
