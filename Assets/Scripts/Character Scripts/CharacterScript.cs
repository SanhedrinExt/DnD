using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterScript : NetworkBehaviour {

    [SyncVar(hook="OnCharacterHit")]
    private int m_Health = 10;

    private Renderer m_Renderer;

    private Transform m_NameTagSpace;
    private Transform m_NameTag;

	// Use this for initialization
	protected virtual void Start () {
        m_Renderer = GetComponent<Renderer>();

        m_NameTag = transform.FindChild("NameTag");
        m_NameTagSpace = GameObject.Find("Name Space").transform;
        transform.SetParent(m_NameTagSpace);


        if (!isServer && !isLocalPlayer)
        {
            m_Renderer.enabled = false;
        }

        foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, Vector3.zero, 1))
        {
            VisitableObject room = hit.collider.transform.GetComponent<VisitableObject>();

            if (room)
            {
                room.OnTriggerEnter2D(GetComponent<Collider2D>());
                break;
            }
        }
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (isLocalPlayer && m_NameTag)
        {
            m_NameTag.transform.position = transform.position + Vector3.up * (transform.localScale.y / 2);
        }
	}

    /// <summary>
    /// Syncvar hooks are called on the clients before being changed.
    /// We'll use this to check if the character should be destroyed locally.
    /// </summary>
    [Client]
    private void OnCharacterHit(int i_NewHealth)
    {
        m_Health = i_NewHealth;

        if (m_Health <= 0)
        {
            KillCharacter();
        }
    }

    [Command]
    public void CmdDamageCharacter(int i_Damage)
    {
        m_Health -= i_Damage;
    }

    private void KillCharacter()
    {
        //TODO: Implement character death
    }
}
