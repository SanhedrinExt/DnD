﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// This entire script is done only locally to manage which objects each client can see.
/// </summary>
public abstract class VisitableObject : NetworkBehaviour
{
    public Renderer m_Renderer;

    private bool m_HasBeenVisited;
    public bool HasbeenVisited
    {
        get
        {
            return m_HasBeenVisited;
        }
        set
        {
            m_HasBeenVisited = value;
            m_Renderer.enabled = m_HasBeenVisited ? true : false;
        }
    }

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    protected virtual void Start()
    {
        if (!isServer)
        {
            m_Renderer.enabled = false;
        }
    }

    protected virtual void Update()
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D i_Collider)
    {
        CharacterScript charScript = i_Collider.GetComponent<CharacterScript>();
        
        //When a player enters a visible room, make the player visible and otherwise.
        if (m_HasBeenVisited)
        {
            i_Collider.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            if (charScript.isLocalPlayer)
            {
                m_Renderer.enabled = true;
            }
            else
            {
               i_Collider.GetComponent<Renderer>().enabled = false;
            }
        }

        RoomScript room = GetComponent<RoomScript>();
        if (room && charScript.isLocalPlayer)
        {
            room.ShowRoomObjacts(true);
        }

        if (charScript.isLocalPlayer)
        {
            m_HasBeenVisited = true;
        }
    }
}
