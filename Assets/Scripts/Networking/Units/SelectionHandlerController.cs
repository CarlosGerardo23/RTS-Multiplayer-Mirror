using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionHandlerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _layerMask;
    private Camera _mainCamera;
    public List<UnitNetworkController> SelectedUnits {get;private set;}= new List<UnitNetworkController>();
   
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        _inputReader.OnPlayerSelect += OnTrySelectUnit;
        _inputReader.OnPlayerDeselect += DeselectUnits;
    }
    private void OnDisable()
    {
        _inputReader.OnPlayerSelect -= OnTrySelectUnit;
        _inputReader.OnPlayerDeselect -= DeselectUnits;
    }
    private void OnTrySelectUnit()
    {
        AnalizeSelectionArea();
    }

    private void AnalizeSelectionArea()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_inputReader.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            if (hit.collider.TryGetComponent(out UnitNetworkController unit))
            {
                if (unit.isOwned)
                    SelectedUnits.Add(unit);
            }
        }
        for (int i = 0; i < SelectedUnits.Count; i++)
            SelectedUnits[i].Select();

    }
    private void DeselectUnits()
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
            SelectedUnits[i].Deselect();
        SelectedUnits.Clear();
    }
}
