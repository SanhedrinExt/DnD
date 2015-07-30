using UnityEngine;
using System.Collections;

public class BfsNode {

    public Vector2 perent;
    public Vector2 mySpot;
    public int depth;
    
    public BfsNode(Vector2 me,Vector2 per ,int dep )
    {
        mySpot = me; perent = per; depth = dep;
    }

	
	
}
