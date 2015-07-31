using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DungeonLobbyNetworkManager : NetworkLobbyManager {

    [SerializeField]
    private GameObject clientPrefab;
    [SerializeField]
    private GameObject serverPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Create lobby player");
        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = null;
        
        if (conn.connectionId == conn.hostId)
        {
            player = Instantiate(serverPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }
        else
        {
            player = Instantiate(clientPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        NetworkServer.ReplacePlayerForConnection(conn, player, playerControllerId);

        return player;
    }
}
