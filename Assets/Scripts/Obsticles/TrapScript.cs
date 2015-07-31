using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

    [SerializeField]
    private const int k_TimeToStun = 4000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D i_CollisionInfo)
    {
        AdventurerScript AdventurerInTrap =  i_CollisionInfo.gameObject.GetComponent<AdventurerScript>();
        AdventurerInTrap.CmdStunAdventurer(k_TimeToStun);
        Destroy(gameObject);
    }
}
