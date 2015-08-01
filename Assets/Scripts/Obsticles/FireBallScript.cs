using UnityEngine;
using System.Collections;
using System;

public class FireBallScript : MonoBehaviour {

    [SerializeField]
    private int m_lifeSpan = 3000;

    [SerializeField]
    private int m_Speed = 10;

    [SerializeField]
    private int m_Damage = 1;

    private DateTime m_StartTime;

    private Rigidbody2D m_RigidBody;

	// Use this for initialization
	void Start () {
        m_StartTime = DateTime.Now;
        m_RigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        TimeSpan lifeTime = DateTime.Now - m_StartTime;
        if (lifeTime.TotalMilliseconds >= m_lifeSpan)
        {
            Destroy(this);
        }
        else
        {
            m_RigidBody.AddForce(m_RigidBody.velocity.normalized * m_Speed);
        }
	}

    public void OnTriggerEnter2D(Collider2D i_CollisionInfo)
    {
        AdventurerScript Adventurer = i_CollisionInfo.gameObject.GetComponent<AdventurerScript>();
        if (Adventurer != null)
        {
            Adventurer.CmdDamageCharacter(m_Damage);
        }
    }
}
