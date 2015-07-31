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
        //return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = null;
        Debug.Log(numPlayers);
        if (numPlayers == 0)
        {
            Debug.Log("Server");
            player = Instantiate(serverPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log("Client");
            player = Instantiate(clientPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        return player;
    }
}
