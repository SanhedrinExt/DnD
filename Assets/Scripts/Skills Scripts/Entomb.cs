using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SkillProvider;

public class Entomb : Skill 
{
    private const int k_Cooldown = 10;

    public Entomb() : base(k_Cooldown) { }

    protected override void Activate()
    {
        //TODO...
    }
    
    // Use this for initialization
	void Start () {
	
	}
}
