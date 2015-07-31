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
        if (isLocalPlayer)
        {
            if (ControlsManager.TapActionRequested())
            {
                Vector3 moveTo = ControlsManager.GetTapActionPoint();
                CmdMovementManagement(moveTo);
            }
        }

	    if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            CmdMovementManagement(new Vector3(0.19f, -1.64f, 0f));
        }
	}

    [Command]
    private void CmdMovementManagement(Vector3 i_MoveTo)
    {
        Stack<Vector3> roomPathToTarget = Graph.GraphSingleton.GetVectorPath(transform.position, i_MoveTo);
        StopCoroutine("MovePlayerAlongRoute");
        StartCoroutine(MovePlayerAlongRoute(roomPathToTarget));
    }

    [Server]
    private IEnumerator MovePlayerAlongRoute(Stack<Vector3> i_RoomPath)
    {
        while (i_RoomPath.Count > 0)
        {
            transform.position += (transform.position - i_RoomPath.Peek()).normalized * (Time.deltaTime * m_MoveSpeed);

            if (Vector3.Distance(transform.position, i_RoomPath.Peek()) < 0.1f)
            {
                i_RoomPath.Pop();
            }

            yield return null;
        }

        CheckAttack(transform.position);
    }
}
