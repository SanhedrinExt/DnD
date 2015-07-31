using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public static class ControlsManager
{
    public static bool TapActionRequested()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonUp(0) && !CameraControls.s_InMovement;
            
#elif UNITY_ANDROID || UNITY_IOS
        return Input.GetTouch(0).phase == TouchPhase.Ended && !CameraControls.s_InMovement;
#endif
    }

    public static Vector3 GetTapActionPoint()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
#elif UNITY_ANDROID || UNITY_IOS
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
    }
}

