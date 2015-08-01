using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;


[AddComponentMenu("UI/Camera Controller")]
public class CameraControls : MonoBehaviour {

    public static Vector3 StartPosition { get; private set; }
    private float m_StartDistance;
    private float m_LastDistance;

    public static bool s_InMovement = false;

    //Swipe controls
    public static DateTime StartPositionTime { get; private set; }

    public bool InMovement { get { return s_InMovement; } }

    [SerializeField]
    private float m_CameraVelocity;

    [SerializeField]
    private float m_MinDistance;

    [SerializeField]
    private float m_ZoomSpeed;

    [SerializeField]
    private float m_RotationSpeed;

	// Use this for initialization
	void Start () {
        Random.seed = DateTime.Now.Millisecond * DateTime.Now.Millisecond / DateTime.Now.Second;
	}

	
	// Update is called once per frame
	void Update () {
        checkMoveCamera();
        checkZoomCamera();
    }


    private void checkZoomCamera()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        float zoom = Input.GetAxis("Mouse ScrollWheel") * m_ZoomSpeed;

        zoomCamera(zoom);
#elif UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount > 1){
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                m_LastDistance = m_StartDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
               float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
               if(Mathf.Abs(distance - m_LastDistance) >= m_MinDistance / 10){
                   float zoom = Mathf.Sign(distance - m_LastDistance) * m_ZoomSpeed;
                   m_LastDistance = distance;

                   zoomCamera(zoom);
               }
            }
        }
#endif
    }

    private void checkMoveCamera()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        //Input.mousePosition
        if (Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
            StartPositionTime = DateTime.Now;
        }
        else if (Input.GetMouseButton(0))
        {
            moveCamera(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            s_InMovement = false;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount == 1){
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartPosition = Input.GetTouch(0).position;
                StartPositionTime = DateTime.Now;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
               moveCamera(Input.GetTouch(0).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
               s_InMovement = false;
            }
        }
#endif
    }

    private void moveCamera(Vector3 i_EndPosition)
    {
        if (Vector3.Distance(StartPosition, i_EndPosition) >= m_MinDistance)
        {
            transform.position -= (i_EndPosition - StartPosition).normalized * m_CameraVelocity;
            s_InMovement = true;
        }
    }

    private void zoomCamera(float i_Zoom)
    {
        Camera.main.orthographicSize = Camera.main.orthographicSize - i_Zoom >= 1 ? Camera.main.orthographicSize - i_Zoom : Camera.main.orthographicSize;
    }
}
