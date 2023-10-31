using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine;

public class UnitSpawnerBuildingController : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform _unitSpawnPointTransform;


    #region Server
    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unit = Instantiate(_unitPrefab, _unitSpawnPointTransform.position, _unitSpawnPointTransform.rotation);
        NetworkServer.Spawn(unit, connectionToClient);
    }
    #endregion

    #region Client
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOwned) return;
        if (eventData.button == PointerEventData.InputButton.Left)
            CmdSpawnUnit();

    }
    #endregion
}
