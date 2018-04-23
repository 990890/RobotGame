using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficManager : MonoBehaviour {
    // timer for lights in second.
    [SerializeField, Range(1f, 20f)]
    float m_TrafficTimer = 5;
    // Float to store the Time since light Change
    private float m_Time;    

    // the stop barries are empty gameobjects with only a box collider these stop the traffice objects
    private BoxCollider[] m_StopBarriers;
    // 
    public void ToggleLights()
    {
        foreach (BoxCollider Barrier in m_StopBarriers)
        {
            if (Barrier.enabled == true)
            {
                Barrier.enabled = false;
                foreach (var item in Barrier.GetComponentsInChildren<Light>())
                {
                    item.color = Color.red;
                }

            }
            else {
                Barrier.enabled = true;
                foreach (var item in Barrier.GetComponentsInChildren<Light>())
                {
                    item.color = Color.green;
                }
            }
        }
        m_Time = Time.timeSinceLevelLoad;
    }

    // Use this for initialization
    void Start () {
        m_StopBarriers = GetComponentsInChildren<BoxCollider>();
        // get time to compare to Light change.
        m_Time = Time.timeSinceLevelLoad;
        // initalize the lights
        foreach (BoxCollider Barrier in m_StopBarriers)
        {
            if (Barrier.tag == "TrafficLeftRight") Barrier.enabled = false;
            else Barrier.enabled = true;
        }
        ToggleLights();
    }

    // Update is called once per frame
    void Update () {
        // call the spawn method if the spawnner time has passed

        if (m_Time + m_TrafficTimer < Time.timeSinceLevelLoad)
        {
            ToggleLights();
        }

    }


}
