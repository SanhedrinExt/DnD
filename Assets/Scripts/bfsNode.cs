using UnityEngine;
using System.Collections;

public class BfsNode {

    public BfsNode perent;
    public Vector2 mySpot;
    public int depth;
    
    public BfsNode(Vector2 me,BfsNode per ,int dep )
    {
        mySpot = me; perent = per; depth = dep;
    }

	
	
}
