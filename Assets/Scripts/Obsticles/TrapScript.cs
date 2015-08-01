using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

    [SerializeField]
    private int m_TimeToStun = 4000;

    [SerializeField]
    private int m_Damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D i_CollisionInfo)
    {
        AdventurerScript AdventurerInTrap =  i_CollisionInfo.gameObject.GetComponent<AdventurerScript>();
        AdventurerInTrap.CmdStunAdventurer(m_TimeToStun);
        AdventurerInTrap.CmdDamageCharacter(m_Damage);
        Destroy(gameObject);
    }
}
