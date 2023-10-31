using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

[CreateAssetMenu(menuName ="Input")]
public class InputReader : ScriptableObject, Inputs.IPlayerActions
{
    #region Player Inputs Events
    public Action OnPlayerSelect = default;
    #endregion
    private Inputs _inputs;
    private void OnEnable()
    {
        if (_inputs == null)
        {
            _inputs = new Inputs();
            _inputs.Player.SetCallbacks(this);
        }
    }
    #region Global Inputs
    public Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }
    #endregion
    #region Player Inputs
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            OnPlayerSelect();

    }
    #endregion

    #region Enable Inputs
    public void EnablePlayerInputs()
    {
        _inputs.Player.Enable();
    }
    #endregion
}
