using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

[AddComponentMenu("Skills/Place Trap")]
public class PlaceTrap : StackableSkill
{
    [SerializeField]
    private const int k_Cooldown = 3000;
    [SerializeField]
    private const int k_MaxStacks = 4;
    [SerializeField]
    GameObject m_TrapPrefab;

    public PlaceTrap() : base(k_Cooldown, k_MaxStacks) { }

    protected sealed override void Activate(Vector3 i_Position)
    {
        GameObject trap = GameObject.Instantiate(m_TrapPrefab);
        Debug.Log("instantiated trap");
        trap.transform.position = i_Position;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}
