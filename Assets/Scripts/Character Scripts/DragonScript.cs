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

    [SerializeField]
    GameObject dragonSkillsMenu;

    public DragonScript()
    {
        SelectedSkill = 0;
    }

    protected override void Start()
    {
        if (!isServer)
        {
            dragonSkillsMenu.SetActive(false);
        }

        m_RemoveAddRoom = GetComponent<RemoveAddRoom>();
        m_PlaceTrap = GetComponent<PlaceTrap>();
        m_PlaceDragonling = GetComponent<PlaceDragonling>();
        m_FireBreath = GetComponent<FireBreath>();
        m_Entomb = GetComponent<Entomb>();

        base.Start(); 
    }

    protected override void Update()
    {
        if (ControlsManager.TapActionRequested())
	    {
		    Vector3 tapVector = ControlsManager.GetTapActionPoint();
            TimeSpan timeFromLastSkillPick = DateTime.Now - m_LastPick;

            if (SelectedSkill != eSkill.ePlaceDragonlingClicked && timeFromLastSkillPick.TotalMilliseconds >= 50)
            {
                switch (SelectedSkill)
                {
                    case eSkill.eAddRemoveRoomClicked:
                        if (!PlayerInRoom(tapVector))
                        {
                            m_RemoveAddRoom.CmdUseSkill(tapVector);
                        }
                        break;
                    case eSkill.ePlaceTrapClicked:
                        m_PlaceTrap.CmdUseSkill(tapVector);
                        break;
                    case eSkill.eEntombClicked:
                        m_Entomb.CmdUseSkill(tapVector);
                        break;
                    case eSkill.eFireBreathClicked:
                        m_FireBreath.CmdUseSkill(tapVector);
                        break;
                    default:
                        break;
                }
                SelectedSkill = 0;
            }
	    }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && (DateTime.Now - CameraControls.StartPositionTime).TotalMilliseconds <= 500.0)
        {
            Vector3 position = Input.GetTouch(0).position;
            Debug.Log("why???");
            if (SelectedSkill == eSkill.ePlaceDragonlingClicked && Vector3.Distance(position, CameraControls.StartPosition) > 5f)
            {
                m_PlaceDragonling.CmdUseSkill(Camera.main.ScreenToWorldPoint(CameraControls.StartPosition));
            }
        }
        base.Update();
    }

    private bool PlayerInRoom(Vector3 i_Position)
    {
        bool isInRoom = false;
        RoomScript room = Graph.GraphSingleton.ConvertV3ToRoomScript(i_Position);
        if (room == null)
        {
            return true;
        }
        Vector2 v3 = new Vector2(room.transform.position.x, room.transform.position.y);
        Vector2 s2 = new Vector2(room.transform.localScale.x, room.transform.localScale.y);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(v3, s2, 0, Vector2.up, s2.x / 2, 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Dragon"));

        if (hits.Length > 0)
            isInRoom = true;

        return isInRoom;
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