using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph {

    public const int c_y = 7;
    public const int c_x = 7;

    public RoomNode[,] m_rooms = new RoomNode[c_y, c_x];

    public void RemovRoom(Vector2 i_roomVec)
    {
        RoomNode roomToremove = m_rooms[(int)i_roomVec.y, (int)i_roomVec.x];
        roomToremove.DeactivatRoom();
        
        while(IsGrathConacted() == false) // בהחרך קיים שחן שחור ואחד לבן
        {
            ConactNaibers(roomToremove);
        }
    }

    public void ConactNaibers(RoomNode i_badRoom)
    {
       
        foreach(RoomNode room in i_badRoom.m_niebringRooms)
        {
            if(room.m_eColor == eColor.White)
            {
                ConactMe(room.myPosition, i_badRoom.myPosition);
                break;
            }
        }
       
    }

    public void ConactMe(Vector2 S, Vector2 i_badRoom)
    {
        Queue<BfsNode> queue = new Queue<BfsNode>();
        List<Vector2> arcivs = new List<Vector2>();
        arcivs.Add(S);
        queue.Enqueue(new BfsNode(S, null, 0));
        BfsNode solver = null;

        while(queue.Count != 0)
        {
            BfsNode node = queue.Dequeue();

            List<BfsNode> naeibers = getGoodNaeibersForBfs(node, i_badRoom, arcivs);
            foreach(BfsNode naeiber in naeibers)
            {
                if(m_rooms[(int)naeiber.mySpot.y,(int)naeiber.mySpot.x].m_activRoom == true && m_rooms[(int)naeiber.mySpot.y,(int)naeiber.mySpot.x].m_eColor == eColor.Black)
                {
                    solver = naeiber;
                    break;
                }
                else
                {
                    queue.Enqueue(naeiber);
                }
            }
            if (solver != null) break;

        }
        ReconctingGrath(solver);
    }

private void ReconctingGrath(BfsNode solver)
{
 	throw new System.NotImplementedException();
}

public List<BfsNode> getGoodNaeibersForBfs(BfsNode i_node,Vector2 i_badRoom,List<Vector2> arcivs)
{
    List<BfsNode> nodss = new List<BfsNode>();
    int x = (int)i_node.mySpot.x; 
    int y = (int)i_node.mySpot.y;
    
    return nodss;
}



    public void ResetColor()
    {
        foreach(RoomNode room in m_rooms)
        {
            room.m_eColor = eColor.White;
        }
    }

    public void Visit(RoomNode i_room)
    {
        i_room.m_eColor = eColor.Grey;

        foreach(RoomNode room in i_room.m_niebringRooms)
        {
            if(room.m_eColor == eColor.White)
            {
                Visit(room);
            }
        }
        i_room.m_eColor = eColor.Black;
    }
    
    public void Dfs()
    {
        ResetColor();
        foreach(RoomNode room in m_rooms)
        {
            if(room.m_activRoom == true)
            {
                Visit(room);
            }
        }
    }

    public bool IsGrathConacted()
    {
        bool res = true;

        Dfs();

        foreach (RoomNode room in m_rooms)
        {
            if (room.m_activRoom == true && room.m_eColor == eColor.White)
            {
                res = false;
            }
        }   
        return res;
    }
 
	
}
