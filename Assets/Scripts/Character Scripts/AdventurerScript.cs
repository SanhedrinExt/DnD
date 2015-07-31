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
#if UNITY_EDITOR || UNITY_STANDALONE
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonUp(0))
            {

            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount > 1){
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                m_LastDistance = m_StartDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
               float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
               if(Mathf.Abs(distance - m_LastDistance) >= m_MinDistance / 10){
                   float zoom = Mathf.Sign(distance - m_LastDistance) * m_ZoomSpeed;
                   m_LastDistance = distance;

                   zoomCamera(zoom);
               }
            }
        }
#endif
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
