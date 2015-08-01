using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[AddComponentMenu("Dungeon/Room Script")]
public class RoomScript : VisitableObject {

    public RoomNode roomGraphNode;

    [SerializeField]
    private Vector2 m_RoomGridPosition;

	// Use this for initialization
    sealed protected override void Start()
    {
        Graph.GraphSingleton.ConactRoomScripToRoomNode(m_RoomGridPosition, this);

        if (!m_Renderer.enabled)
        {
            m_Renderer.enabled = true;
            gameObject.SetActive(false);
        }
        else
        {
            Graph.GraphSingleton.m_rooms[(int)m_RoomGridPosition.y,(int) m_RoomGridPosition.x].m_activRoom = true;
        }

        base.Start();
	}
	
	// Update is called once per frame
    sealed protected override void Update()
    {
        base.Update();
	}

    public void ShowRoomObjacts(bool i_ShowRoomObjacts)
    {
        Vector2 v3 = new Vector2(transform.position.x, transform.position.y);
        Vector2 s2 = new Vector2(transform.localScale.x, transform.localScale.y);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(v3, s2, 0, Vector2.up, s2.x / 3, 1 << LayerMask.NameToLayer("Door") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Dragon"));

        foreach (RaycastHit2D hit in hits)
        {
            Renderer randerer = hit.transform.GetComponent<Renderer>();
            if (randerer)
            {
                randerer.enabled = i_ShowRoomObjacts;
            }
        }
    }

}
