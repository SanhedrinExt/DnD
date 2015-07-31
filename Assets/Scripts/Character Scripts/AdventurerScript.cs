using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class AdventurerScript : PlayerScript
{
    [SerializeField]
    private float m_MoveSpeed = 10;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (isLocalPlayer)
        {
            if (ControlsManager.TapActionRequested())
            {
                Vector3 moveTo = ControlsManager.GetTapActionPoint();
                CmdMovementManagement(moveTo);
            }
        }

	    base.Update();
	}

    [Command]
    private void CmdMovementManagement(Vector3 i_MoveTo)
    {
        Stack<Vector3> roomPathToTarget = Graph.GraphSingleton.GetVectorPath(transform.position, i_MoveTo);
        StopAllCoroutines();
        StartCoroutine(MovePlayerAlongRoute(roomPathToTarget));
    }

    [Server]
    private IEnumerator MovePlayerAlongRoute(Stack<Vector3> i_RoomPath)
    {
        while (i_RoomPath.Count > 0)
        {
            Vector3 moveDir = (i_RoomPath.Peek() - transform.position).normalized;
            transform.position += moveDir * (Time.deltaTime * m_MoveSpeed);

            if (Vector3.Distance(transform.position, i_RoomPath.Peek()) < 0.1f)
            {
                i_RoomPath.Pop();
            }

            yield return null;
        }

        CheckAttack(transform.position);
    }
}
