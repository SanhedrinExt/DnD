using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[AddComponentMenu("Dungeon/Door Script")]
public class DoorScript : VisitableObject {

    [SerializeField]
    private Vector2 m_Room1GridPosition;

    [SerializeField]
    private Vector2 m_Room2GridPosition;

    protected override void Start()
    {
        if (!m_Renderer.enabled)
        {
            m_Renderer.enabled = true;
            gameObject.SetActive(false);
        }
        else
        {
            Graph.GraphSingleton.ConactingRooms(m_Room1GridPosition, m_Room2GridPosition);
        }

        base.Start();
    }

	// Update is called once per frame
	protected override void Update () {
	    //TODO: Check if either end of this door is an active room, and if so enable its' renderer.
        //Otherwise, disable it.
        base.Update();
	}

    public override void OnTriggerEnter2D(Collider2D i_Collider)
    {
        i_Collider.GetComponent<Animator>().SetBool("Open", true);

        base.OnTriggerEnter2D(i_Collider);
    }

    public void OnTriggerExit2D(Collider2D i_Collider)
    {
        i_Collider.GetComponent<Animator>().SetBool("Open", false);

    }

  
}
