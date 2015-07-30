using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

public class FireBreath : Skill
{
    private const int k_Cooldown = 5;

    public FireBreath() : base(k_Cooldown) { }

    protected override void Activate()
    {
        //TODO...
    }

    // Use this for initialization
    void Start()
    {

    }
}
