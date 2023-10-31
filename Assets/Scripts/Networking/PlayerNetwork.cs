using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private Renderer _playerRenderer;
    [SyncVar(hook = nameof(HandleDisplayPlayeName))]
    [SerializeField] private string _playerName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColor))]
    [SerializeField] private Color _playerColor = Color.black;
    #region Server
    [Server]
    public void SetPlayerName(string playerName)
    {
        _playerName = playerName;
    }
    [Server]
    public void SetColor(Color color)
    {
        _playerColor = color;

    }
    [Command]
    private void CmdSetPlayerName(string newName)
    {
        if (newName.Contains(" ") || newName.Length > 10)
            return;
        RpcLogNewName(newName);
        SetPlayerName(newName);
    }

    #endregion
    #region #Client
    private void HandleDisplayPlayeName(string oldName, string newName)
    {
        _playerNameText.text = newName;
    }
    private void HandleDisplayColor(Color oldColor, Color newColor)
    {
        print($"New color {newColor}");
        _playerRenderer.material.SetColor("_BaseColor", newColor);
    }
    [ContextMenu("Set my name")]
    private void SetMyName()
    {
        CmdSetPlayerName("My new name");
    }
    [ClientRpc]
    private void RpcLogNewName(string newName)
    {
        Debug.Log($"Server name is: {newName}");
    }
    #endregion
}
