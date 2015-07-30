using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScript : CharacterScript
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    private void CmdMovementManagement(Vector2 i_MoveTo)
    {
        //TODO: Implement movement once rooms are available.
    }
}
