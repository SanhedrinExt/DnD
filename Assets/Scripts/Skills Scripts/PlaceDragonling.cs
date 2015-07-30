using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

public class PlaceDragonling : StackableSkill
{
    [SerializeField]
    private const int k_Cooldown = 15000;
    [SerializeField]
    private const int k_MaxStacks = 4;

    public PlaceDragonling() : base(k_Cooldown, k_MaxStacks) { }

    protected sealed override void Activate()
    {
        // TODO...    
    }

    // Use this for initialization
    void Start()
    {

    }
}
