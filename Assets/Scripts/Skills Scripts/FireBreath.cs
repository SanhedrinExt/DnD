using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

[AddComponentMenu("Skills/Fire Breath")]
public class FireBreath : Skill
{
    private const int k_Cooldown = 5000;

    public FireBreath() : base(k_Cooldown) { }

    protected override void Activate(Vector3 i_Position)
    {
        //TODO...
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
