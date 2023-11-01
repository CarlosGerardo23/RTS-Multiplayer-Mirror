using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class UnitCommanderController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _layerMask;
    private Camera _mainCamera;
    private SelectionHandlerController _selectionHandler;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _selectionHandler = FindObjectOfType<SelectionHandlerController>();
    }
    private void OnEnable()
    {
        _inputReader.EnablePlayerInputs();
        _inputReader.OnPlayerSelect += TryPerformCommand;
    }
    private void OnDisable()
    {
        _inputReader.OnPlayerSelect -= TryPerformCommand;
    }
    private void TryPerformCommand()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_inputReader.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity, _layerMask))
        {
            for (int i = 0; i < _selectionHandler.SelectedUnits.Count; i++)
            {
                _selectionHandler.SelectedUnits[i].GetComponent<PlayerNetworkMovementController>().CmdMove(hit.point);
            }
        }
    }
}
