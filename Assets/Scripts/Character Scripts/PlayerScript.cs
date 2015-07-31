using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class PlayerScript : CharacterScript {

    [SerializeField]
    private float m_AttackRange;

    [SerializeField]
    private int m_AttackPower;

    [Server]
    protected void CheckAttack(Vector3 i_TargetPosition)
    {
        if (Vector3.Distance(i_TargetPosition, transform.position) <= m_AttackRange)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(i_TargetPosition, Vector3.zero, 1);

            foreach (RaycastHit2D hit in hits)
            {
                CharacterScript charScript = hit.transform.GetComponent<CharacterScript>();
                if (hit.transform != transform && charScript)
                {
                    charScript.CmdDamageCharacter(m_AttackPower);
                    break;
                }
            }
        }
    }
}
