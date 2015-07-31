using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class DragonlingScript : NetworkBehaviour
{

    [SerializeField]
    private const int k_FireFrequency = 2000;

    [SerializeField]
    private GameObject m_FireBallPrefab = null;

    private DateTime m_lastFireTime;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            m_lastFireTime = DateTime.Now;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            TimeSpan deltaTime = DateTime.Now - m_lastFireTime;
            if ((int)deltaTime.TotalMilliseconds >= k_FireFrequency)
            {
                m_lastFireTime = DateTime.Now;
                fire();
            }
        }
    }

    private void fire()
    {
        GameObject f = Instantiate(m_FireBallPrefab);
    }
}
