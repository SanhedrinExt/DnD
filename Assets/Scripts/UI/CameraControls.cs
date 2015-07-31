using UnityEngine;
using System.Collections.Generic;


public class CameraControls : MonoBehaviour {

    private Vector3 m_StartPosition;

    [SerializeField]
    private float m_CameraVelocity;

    [SerializeField]
    private float m_MinDistance;

	// Use this for initialization
	void Start () {
      
	}

	
	// Update is called once per frame
	void Update () {
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
        
#elif UNITY_ANDROID  
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            m_StartPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
           moveCamera(Input.GetTouch(0).position);
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
}
