using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public enum eColor { White,Black,Grey}

public class RoomNode  {

    public eColor m_eColor;

    public RoomScript refRoom; // to do

    public bool m_activRoom;
    public List<RoomNode> m_niebringRooms;
    public Vector2 myPosition;

    public RoomNode(int x,int y)
    {
        m_niebringRooms = new List<RoomNode>();
        myPosition = new Vector2(x, y);
    }

    public void removNiber(RoomNode i_badNiber)
    {
        m_niebringRooms.Remove(i_badNiber);
    }
    public void DeactivatRoom()
    {
        foreach(RoomNode niebr in m_niebringRooms)
        {
            niebr.removNiber(this);
        }
        m_activRoom = false;
        refRoom.rpcEnabalRoom(false);
    }

    

    public void ActivatRoom(List<RoomNode> i_naebers = null)
    {
        if(i_naebers != null)
        {
            foreach( RoomNode naeber in i_naebers)
            {
                m_niebringRooms.Add(naeber);
                naeber.m_niebringRooms.Add(this);
            }
        }
        m_activRoom = true;
        refRoom.rpcEnabalRoom(true);
    }
}
