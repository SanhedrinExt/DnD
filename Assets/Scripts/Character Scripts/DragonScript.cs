using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Characters/Dragon Script")]
public class DragonScript : PlayerScript
{
    RemoveAddRoom m_RemoveAddRoom;
    PlaceTrap m_PlaceTrap;
    PlaceDragonling m_PlaceDragonling;
    FireBreath m_FireBreath;
    Entomb m_Entomb;
    public eSkill SelectedSkill { get; private set; }

    private DateTime m_LastPick = DateTime.Now;

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
        if (ControlsManager.TapActionRequested())
	    {
		    Vector3 tapVector = ControlsManager.GetTapActionPoint();
            TimeSpan timeFromLastSkillPick = DateTime.Now - m_LastPick;

            if (SelectedSkill != 0 && timeFromLastSkillPick.TotalMilliseconds >= 50)
            {
                switch (SelectedSkill)
                {
                    case eSkill.eAddRemoveRoomClicked:
                        m_RemoveAddRoom.UseSkill(tapVector);
                        break;
                    case eSkill.ePlaceTrapClicked:
                        m_PlaceTrap.UseSkill(tapVector);
                        break;
                    case eSkill.eEntombClicked:
                        m_Entomb.UseSkill(tapVector);
                        break;
                    case eSkill.eFireBreathClicked:
                        m_FireBreath.UseSkill(tapVector);
                        break;
                    default:
                        break;
                }
            }
	    }
    }

    public enum eSkill
    {
        eAddRemoveRoomClicked = 1,
        ePlaceTrapClicked = 2,
        ePlaceDragonlingClicked = 3,
        eEntombClicked = 4,
        eFireBreathClicked = 5
    }

    private void disableAllFlags()
    {
        SelectedSkill = 0;   
    }

    public void OnRemoveAddRoom()
    {
        m_LastPick = DateTime.Now;
        SelectedSkill = eSkill.eAddRemoveRoomClicked;
    }

    public void OnPlaceTrap()
    {
        m_LastPick = DateTime.Now;
        SelectedSkill = eSkill.ePlaceTrapClicked;
    }

    public void OnPlaceDragonling()
    {
        m_LastPick = DateTime.Now;
        SelectedSkill = eSkill.ePlaceDragonlingClicked;
    }

    public void OnEntomb()
    {
        m_LastPick = DateTime.Now;
        SelectedSkill = eSkill.eEntombClicked;
    }

    public void OnFireBreath()
    {
        m_LastPick = DateTime.Now;
        SelectedSkill = eSkill.eFireBreathClicked;
    }
}