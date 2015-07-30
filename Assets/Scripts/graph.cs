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
        queue.Enqueue(new BfsNode(S, null, 0));

        while(queue.Count != 0)
        {
            BfsNode node = queue.Dequeue();

            

        }

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
