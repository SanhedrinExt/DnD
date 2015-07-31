using UnityEngine;
using System.Collections;


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
    public void showRoom()
    {
        
       // this.transform.position
    }
}
