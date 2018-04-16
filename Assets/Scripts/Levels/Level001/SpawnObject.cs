using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    //**********************************************************************//
    // Class Members
    //**********************************************************************//

    // The max time for the random spawner
    public float m_MaxTime = 4f;
    // The min time for the random spawner
    public float m_MinTime = 1.5f;
    // the Speed each Object in this lane.
    public int m_speed = 10;
    // Direction of travel thaT THE Object will move.
    public OBJECTDIRECTION m_Direction = OBJECTDIRECTION.MOVING_LEFT;

    // List of Objects in the Object pool.
    private List<ObjectInfo> m_ObjectPool;
    // Float to store the Time since last spawn
    private float m_Time;
    // Float to store the random time for next spawn.
    private float m_NextSpawn;
    // chack if the spawn is occupied
    private bool m_IsOccupied = false;

    //**********************************************************************//
    // Unity Callback methods
    //**********************************************************************//

    // Use this for initialization
    void Start()
    {
        // get the object pool. 
        m_ObjectPool = FindObjectOfType<ObjectPool>().GetPool();
        // set time to next spawn.
        m_NextSpawn = Random.Range(1f, m_MaxTime);
        // get deltaTime to seed the last spawn.
        m_Time = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // call the spawn method if the spawnner time has passed
        if (m_Time + m_NextSpawn < Time.timeSinceLevelLoad)
        {
            // spawn object
           if (!m_IsOccupied) Spawn();
            // and set new time fro next spawn.
            m_NextSpawn = Random.Range(1f, m_MaxTime);
            // update time.
            m_Time = Time.timeSinceLevelLoad;
        }
    }
    void OnTriggerStay(Collider col)
    {
        int layerMask = 10;
        if (col.gameObject.layer == layerMask)
            m_IsOccupied = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 10) m_IsOccupied = false;
    }

    //**********************************************************************//
    // Custom methods
    //**********************************************************************//
    void Spawn()
    {
        foreach (ObjectInfo obj in m_ObjectPool)
        {
            CarController cc = obj.m_Object.GetComponent<CarController>();
            if (cc.m_CarState == CARSTATE.IDEL)
            {
                cc.m_CarState = CARSTATE.SPAWNING;
                GameObject go = obj.m_Object;
                go.transform.position = transform.position;
                cc.m_Speed = m_speed;
                cc.m_CarDirection = m_Direction;
                return;
            }
        }
        return;
    }


    public void SetVaribles(float maxTime, float minTime, int speed, OBJECTDIRECTION direction)
    {
        //assign the settings for the spawner
        m_MaxTime = maxTime;
        m_MinTime = minTime;
        m_speed = speed;
        m_Direction = direction;
    }
}
