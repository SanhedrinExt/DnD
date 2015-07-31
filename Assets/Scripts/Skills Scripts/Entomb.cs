using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

public class Entomb : Skill 
{
    private const int k_Cooldown = 10000;

    public Entomb() : base(k_Cooldown) { }

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
