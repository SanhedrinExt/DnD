using UnityEngine;
using System.Collections.Generic;


public class CameraControls : MonoBehaviour {

    private Vector3 m_StartPosition;
    private float m_StartDistance;
    private float m_LastDistance;

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
            m_StartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            moveCamera(Input.mousePosition);
        }

#elif UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount == 1){
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                m_StartPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
               moveCamera(Input.GetTouch(0).position);
            }
        }
#endif
    }

    private void moveCamera(Vector3 i_EndPosition)
    {
        if (Vector3.Distance(m_StartPosition, i_EndPosition) >= m_MinDistance)
        {
            transform.position -= (m_StartPosition - i_EndPosition).normalized * m_CameraVelocity;
        }
    }

    private void zoomCamera(float i_Zoom)
    {
        Camera.main.orthographicSize = Camera.main.orthographicSize + i_Zoom >= 1 ? Camera.main.orthographicSize + i_Zoom : Camera.main.orthographicSize;
    }
}
