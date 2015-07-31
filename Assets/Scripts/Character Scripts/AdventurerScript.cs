using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class AdventurerScript : PlayerScript
{
    [SerializeField]
    private float m_MoveSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    private void CmdMovementManagement(Vector3 i_MoveTo)
    {
        Queue<Vector3> roomPathToTarget = null; //RoomGraph.GetVectorPath(transform.position, i_MoveTo);
        StopCoroutine("MovePlayerAlongRoute");
        StartCoroutine(MovePlayerAlongRoute(roomPathToTarget));
    }

    [Server]
    private IEnumerator MovePlayerAlongRoute(Queue<Vector3> i_RoomPath)
    {
        while (i_RoomPath.Count > 0)
        {
            transform.position += (transform.position - i_RoomPath.Peek()).normalized * (Time.deltaTime * m_MoveSpeed);

            if (Vector3.Distance(transform.position, i_RoomPath.Peek()) < 0.1f)
            {
                i_RoomPath.Dequeue();
            }

            yield return null;
        }

        CheckAttack(transform.position);
    }
}
