using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNetworkMovementController : NetworkBehaviour
{
    private NavMeshAgent _agent;
    #region Unity Calls
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
  
    #endregion
    #region Client
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }

    #endregion
    #region  Server
    [Command]
    public void CmdMove(Vector3 position)
    {
        _agent.SetDestination(position);
    }
    #endregion
}
