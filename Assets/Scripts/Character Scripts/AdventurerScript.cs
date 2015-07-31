using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("Characters/Adventurer Script")]
public class AdventurerScript : PlayerScript
{
    [SerializeField]
    private float m_MoveSpeed = 10;

    [SyncVar]
    private bool m_IsStunned = false;

    private DateTime m_StunStartTime;
    private int m_StunDuration;

    private Rigidbody2D m_Rigidbody;

	// Use this for initialization
	protected override void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();

        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        m_Rigidbody.velocity = Vector2.zero;

        if (isServer)
        {
            handleStun();
        }

        if (isLocalPlayer)
        {
            if (ControlsManager.TapActionRequested())
            {
                Vector3 moveTo = ControlsManager.GetTapActionPoint();
                if (!m_IsStunned)
                {
                    CmdMovementManagement(moveTo);
                }
            }
        }

	    base.Update();
	    if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            if (!m_IsStunned)
            {
                CmdMovementManagement(new Vector3(0.19f, -1.64f, 0f));
            }
        }
	}

    private void handleStun()
    {
        if (m_IsStunned)
        {
            TimeSpan TimeInStun = DateTime.Now - m_StunStartTime;
            if ((int)TimeInStun.TotalMilliseconds >= m_StunDuration)
            {
                m_IsStunned = false;
            }
        }
    }

    [Command]
    private void CmdMovementManagement(Vector3 i_MoveTo)
    {
        Stack<Vector3> roomPathToTarget = Graph.GraphSingleton.GetVectorPath(transform.position, i_MoveTo);
        StopAllCoroutines();
        StartCoroutine(MovePlayerAlongRoute(roomPathToTarget));
    }

    [Command]
    public void CmdStunAdventurer(int i_StunDuration)
    {
        m_IsStunned = true;
        m_StunDuration = i_StunDuration;
        m_StunStartTime = DateTime.Now;
     }

    [Server]
    private IEnumerator MovePlayerAlongRoute(Stack<Vector3> i_RoomPath)
    {
        while (i_RoomPath.Count > 0)
        {
            Vector3 moveDir = (i_RoomPath.Peek() - transform.position).normalized;
            m_Rigidbody.MovePosition(transform.position + moveDir * (Time.deltaTime * m_MoveSpeed));

            if (Vector3.Distance(transform.position, i_RoomPath.Peek()) < 0.1f)
            {
                i_RoomPath.Pop();
            }

            yield return null;
        }

        CheckAttack(transform.position);
    }
}
