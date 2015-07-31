using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterScript : NetworkBehaviour {

    [SyncVar(hook="OnCharacterHit")]
    private int m_Health = 10;

    private Renderer m_Renderer;

	// Use this for initialization
	void Start () {
        m_Renderer = GetComponent<Renderer>();

        if (!isServer && !isLocalPlayer)
        {
            m_Renderer.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
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
