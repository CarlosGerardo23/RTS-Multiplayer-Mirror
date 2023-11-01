using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class UnitNetworkController : NetworkBehaviour
{
    [SerializeField] private UnityEvent _onSelected = null;
    [SerializeField] private UnityEvent _onDeselect = null;

    #region Client
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
