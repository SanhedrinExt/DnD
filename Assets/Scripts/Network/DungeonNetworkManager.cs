using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

[AddComponentMenu("Network/Dungeon Network Manager")]
public class DungeonNetworkManager : NetworkManager {
    [SerializeField]
    private GameObject m_ServerPlayer;
    [SerializeField]
    private GameObject m_ClientPlayer;


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

        if (conn.connectionId == conn.hostId)
        {
            NetworkServer.DestroyPlayersForConnection(conn);
            GameObject player = Instantiate(m_ServerPlayer, GameObject.Find("DragonSpawn").transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(player);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }
}
