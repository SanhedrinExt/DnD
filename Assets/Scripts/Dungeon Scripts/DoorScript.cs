using UnityEngine;
using System.Collections;

public class DoorScript : VisitableObject {

    [SerializeField]
    private Vector2 m_Room1GridPosition;

    [SerializeField]
    private Vector2 m_Room2GridPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //TODO: Check if either end of this door is an active room, and if so enable its' renderer.
        //Otherwise, disable it.
	}
}
