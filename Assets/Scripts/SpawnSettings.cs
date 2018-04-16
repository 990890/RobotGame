using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSettings : MonoBehaviour {

    // The max time for the random spawner
    public float m_MaxTime = 4f;
    // The min time for the random spawner
    public float m_MinTime = 1.5f;
    // the Speed each Object in this lane.
    public int m_speed = 10;
    // Direction of travel thaT THE Object will move.
    public OBJECTDIRECTION m_Direction = OBJECTDIRECTION.MOVING_LEFT;

    // the Spawner Object in nthe child gameObject.
    private SpawnObject m_Spawner;

    // Use this for initialization
    void Start () {
        m_Spawner = GetComponentInChildren<SpawnObject>();

        m_Spawner.SetVaribles (m_MaxTime, m_MinTime, m_speed, m_Direction);

    }


}