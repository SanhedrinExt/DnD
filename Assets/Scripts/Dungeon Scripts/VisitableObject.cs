using UnityEngine;
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

    public void OnTriggerEnter2D(Collider2D i_Collider)
    {
        //When a player enters a visible room, make the player visible and otherwise.
        if (m_HasBeenVisited)
        {
            i_Collider.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            CharacterScript charScript = i_Collider.GetComponent<CharacterScript>();

            if (charScript.isLocalPlayer)
            {
                m_Renderer.enabled = true;
            }
            else
            {
                i_Collider.GetComponent<Renderer>().enabled = false;
            }
        }

        if (this is RoomScript)
        {
            SendMessage("showDoor");
        }
    }
}
