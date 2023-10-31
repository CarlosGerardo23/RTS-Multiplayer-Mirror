using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNetworkMovementController : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    #region Unity Calls
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        _inputReader.EnablePlayerInputs();
        _inputReader.OnPlayerSelect += OnPlayerSelect;
    }
    private void OnDisable()
    {
        _inputReader.OnPlayerSelect -= OnPlayerSelect;
    }
    #endregion
    #region Client
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        _mainCamera = Camera.main;
    }
    [ClientCallback]
    private void OnPlayerSelect()
    {
        if (!isOwned)
            return;
        Vector2 mousePosition = _inputReader.GetMousePosition();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CmdMove(hit.point);
        }
    }
    #endregion
    #region  Server
    [Command]
    private void CmdMove(Vector3 position)
    {
        _agent.SetDestination(position);
    }
    #endregion
}
