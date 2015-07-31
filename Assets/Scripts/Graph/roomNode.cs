using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eColor { White,Black,Grey}

public class RoomNode  {

    public eColor m_eColor;

    public RoomScript refRoom; // to do

    public bool m_activRoom;
    public List<RoomNode> m_niebringRooms;
    public Vector2 myPosition;

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
    }


    public RoomNode(){}//to do




    /*
    GameObject refRoom; //todo

    public List<CoradoEge> m_naibringCorador;

    public void AddCorador(CoradoEge i_newCorado)
    {
        m_naibringCorador.Add(i_newCorado);
        return;
    }

    public void addCoradors(List<CoradoEge> i_Coradors)
    {
        foreach(CoradoEge corado in i_Coradors)
        {
            AddCorador(corado);
        }
    }

    public void removCorado(CoradoEge i_coradoToDelete)
    {
        m_naibringCorador.Remove(i_coradoToDelete);
    }
    


	
	*/
}
