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
    public eSkill SelectedSkill { get; private set; }

    public DragonScript()
    {
        SelectedSkill = 0;
    }

    public void Start()
    {
        m_RemoveAddRoom = GetComponent<RemoveAddRoom>();
        m_PlaceTrap = GetComponent<PlaceTrap>();
        m_PlaceDragonling = GetComponent<PlaceDragonling>();
        m_FireBreath = GetComponent<FireBreath>();
        m_Entomb = GetComponent<Entomb>();
    }

    public void Update()
    {
    }

    public enum eSkill
    {
        eAddRemoveRoomClicked,
        ePlaceTrapClicked,
        ePlaceDragonlingClicked,
        eEntombClicked,
        eFireBreathClicked
    }

    private void disableAllFlags()
    {
        SelectedSkill = 0;   
    }

    public void OnRemoveAddRoom()
    {
        SelectedSkill = eSkill.eAddRemoveRoomClicked;
    }

    public void OnPlaceTrap()
    {
        SelectedSkill = eSkill.ePlaceTrapClicked;
    }

    public void OnPlaceDragonling()
    {
        SelectedSkill = eSkill.ePlaceDragonlingClicked;
    }

    public void OnEntomb()
    {
        SelectedSkill = eSkill.eEntombClicked;
    }

    public void OnFireBreath()
    {
        SelectedSkill = eSkill.eFireBreathClicked;
    }
}