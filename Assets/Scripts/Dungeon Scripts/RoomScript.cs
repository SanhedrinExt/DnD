using UnityEngine;
using System.Collections;

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

        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
