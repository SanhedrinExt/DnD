using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

[AddComponentMenu("Skills/Add\\Remove Room")]
public class RemoveAddRoom : StackableSkill
{
    [SerializeField]
    private const int k_Cooldown = 3000;
    [SerializeField]
    private const int k_MaxStacks = 4;

    public RemoveAddRoom() : base(k_Cooldown, k_MaxStacks) { }

    protected sealed override void Activate(Vector3 i_Position)
    {
        // TODO... 
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
