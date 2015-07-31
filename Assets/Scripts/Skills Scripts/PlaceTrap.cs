using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

public class PlaceTrap : StackableSkill
{
    [SerializeField]
    private const int k_Cooldown = 15000;
    [SerializeField]
    private const int k_MaxStacks = 4;

    public PlaceTrap() : base(k_Cooldown, k_MaxStacks) { }

    protected sealed override void Activate()
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
