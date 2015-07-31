using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[AddComponentMenu("Network/Dungeon Lobby Network Manager")]
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
            player = Instantiate(serverPrefab, startPositions[Random.Range(0, startPositions.Count)].position, Quaternion.identity) as GameObject;
        }
        else
        {
            player = Instantiate(clientPrefab, startPositions[Random.Range(0, startPositions.Count)].position, Quaternion.identity) as GameObject;
        }

        //NetworkServer.DestroyPlayersForConnection(conn);
        //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.ReplacePlayerForConnection(conn, player, playerControllerId);

        return player;
    }
}
