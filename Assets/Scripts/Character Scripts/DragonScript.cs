using UnityEngine;
using System.Collections;
using System;

public class DragonScript : PlayerScript
{
    RemoveAddRoom m_RemoveAddRoom;
    PlaceTrap m_PlaceTrap;
    PlaceDragonling m_PlaceDragonling;
    FireBreath m_FireBreath;
    Entomb m_Entomb;

    public override void Start()
    {
        base.Start();
        m_RemoveAddRoom = GetComponent<RemoveAddRoom>();
        m_PlaceTrap = GetComponent<PlaceTrap>();
    }

    public override void Update()
    {
        base.Update();
    }

    enum eSkill
    {
        eAddRemoveRoomClicked,
        ePlaceTrapClicked,
        ePlaceDragonlingClicked,
        eEntombClicked,
        eFireBreathClicked
    }

    private eSkill m_SelectedSkill = 0;

    private void disableAllFlags()
    {
        m_SelectedSkill = 0;   
    }

    public void OnRemoveAddRoom()
    {
        m_SelectedSkill = eSkill.eAddRemoveRoomClicked;
    }

    public void OnPlaceTrap()
    {
        m_SelectedSkill = eSkill.ePlaceTrapClicked;
    }

    public void OnPlaceDragonling()
    {
        m_SelectedSkill = eSkill.ePlaceDragonlingClicked;
    }

    public void OnEntomb()
    {
        m_SelectedSkill = eSkill.eEntombClicked;
    }

    public void OnFireBreath()
    {
        m_SelectedSkill = eSkill.eFireBreathClicked;
    }
}
