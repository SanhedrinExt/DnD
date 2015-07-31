using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DungeonLobbyNetworkManager : NetworkLobbyManager {

    [SerializeField]
    private GameObject clientPrefab;
    [SerializeField]
    private GameObject serverPrefab;

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = null;
        
        if (conn.connectionId == conn.hostId)
        {
            Debug.Log("Dragon connected");
            player = Instantiate(serverPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log("Adventurer connected");
            player = Instantiate(clientPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        //NetworkServer.DestroyPlayersForConnection(conn);
        //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.ReplacePlayerForConnection(conn, player, playerControllerId);

        return player;
    }
}
