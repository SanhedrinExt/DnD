using UnityEngine;
using System.Collections;

public class DoorScript : VisitableObject {

    [SerializeField]
    private Vector2 m_Room1GridPosition;

    [SerializeField]
    private Vector2 m_Room2GridPosition;

    protected override void Start()
    {
        base.Start();
        Graph.GraphSingleton.ConactingRooms(m_Room1GridPosition, m_Room2GridPosition);
    }

	// Update is called once per frame
	protected override void Update () {
	    //TODO: Check if either end of this door is an active room, and if so enable its' renderer.
        //Otherwise, disable it.
        base.Update();
	}
}
