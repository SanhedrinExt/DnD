using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public abstract class CharacterScript : NetworkBehaviour {

    [SyncVar(hook="OnCharacterHit")]
    private int m_Health = 5;

    protected Animator m_Animator;
    private Renderer m_Renderer;

    private Transform m_NameTagSpace;
    private Transform m_NameTag;
    private Transform m_HealthBar;


	// Use this for initialization
	protected virtual void Start () {
        m_Renderer = GetComponent<Renderer>();
        m_Animator = GetComponent<Animator>();

        m_NameTag = transform.FindChild("NameTag");
        m_HealthBar = transform.FindChild("HealthBar");
        if (m_NameTag && m_HealthBar)
        {
            m_NameTagSpace = GameObject.Find("Name Space").transform;
            transform.SetParent(m_NameTagSpace);
        }

        if (!isServer && !isLocalPlayer)
        {
            m_Renderer.enabled = false;
        }
        else
        {
            m_Renderer.enabled = true;
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
        if (isLocalPlayer)
        {
            if (m_NameTag)
            {
                m_NameTag.transform.position = transform.position + Vector3.up / 4;
            }
            if (m_HealthBar)
            {
                m_HealthBar.transform.position = transform.position - Vector3.up / 4;
            }
        }

        if (isServer || isLocalPlayer)
        {
            m_Renderer.enabled = true;
        }

        if (m_NameTag)
        {
            m_NameTag.gameObject.SetActive(m_Renderer.enabled);
        }
        if (m_HealthBar)
        {
            m_HealthBar.gameObject.SetActive(m_Renderer.enabled);
        }
	}

    /// <summary>
    /// Syncvar hooks are called on the clients before being changed.
    /// We'll use this to check if the character should be destroyed locally.
    /// </summary>
    [Client]
    private void OnCharacterHit(int i_NewHealth)
    {
        if (m_HealthBar)
        {
            m_HealthBar.FindChild(string.Format("{0}Health", m_Health + 1)).GetComponent<RawImage>().enabled = false;
            m_HealthBar.FindChild(string.Format("{0}Health", m_Health)).GetComponent<RawImage>().enabled = true;
        }

        m_Health = i_NewHealth < 0 ? m_Health = 0 : i_NewHealth;

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
        m_Animator.SetBool("Hit", true);
    }
}
