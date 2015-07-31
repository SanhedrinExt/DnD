using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public abstract class CharacterScript : NetworkBehaviour {

    [SyncVar(hook="OnCharacterHit")]
    private int m_Health = 5;

    private Renderer m_Renderer;

    private Transform m_NameTagSpace;
    private Transform m_NameTag;
    private Transform m_HealthBar;


	// Use this for initialization
	protected virtual void Start () {
        m_Renderer = GetComponent<Renderer>();

        m_NameTag = transform.FindChild("NameTag");
        m_NameTag = transform.FindChild("HealthBar");
        m_NameTagSpace = GameObject.Find("Name Space").transform;
        transform.SetParent(m_NameTagSpace);

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
        if (isLocalPlayer && m_NameTag)
        {
            m_NameTag.transform.position = transform.position + Vector3.up * (transform.localScale.y / 2);
            m_HealthBar.transform.position = transform.position - Vector3.up * (transform.localScale.y / 2);
        }

        if (isServer || isLocalPlayer)
        {
            m_Renderer.enabled = true;
        }

        m_NameTag.gameObject.SetActive(m_Renderer.enabled);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdDamageCharacter(1);
        }
	}

    /// <summary>
    /// Syncvar hooks are called on the clients before being changed.
    /// We'll use this to check if the character should be destroyed locally.
    /// </summary>
    [Client]
    private void OnCharacterHit(int i_NewHealth)
    {
        m_HealthBar.FindChild(string.Format("{0}Health", m_Health)).GetComponent<RawImage>().enabled = false;

        m_Health = i_NewHealth < 0 ? m_Health = 0 : i_NewHealth;

        m_HealthBar.FindChild(string.Format("{0}Health", m_Health)).GetComponent<RawImage>().enabled = true;

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
