using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Note Comment 
/// </summary>
public class  ToggleTraffic : Interactive {
    public bool m_IsPressed = false;
    public float m_Delay = 3f;
    // the trafic managment script.
    public TraficManager m_TrafficManger;
    //box collider of the barrier that is the parent object of this object.
    BoxCollider m_Barrier;
    Canvas canvas;
    bool m_IsTriggerOccupied = false;

    // Use this for initialization
    void Start () {
        canvas = GetComponentInChildren<Canvas>();
        m_Barrier = GetComponentInParent<BoxCollider>();
        if (m_TrafficManger == null)
        {
            var tm = FindObjectOfType<TraficManager>();
            if (tm != null) m_TrafficManger = tm;
            else Debug.LogError("No Traffice Manger script found in scene.");
        }
	}

	// Update is called once per frame
	void Update () {
        if (m_IsTriggerOccupied && Input.GetKeyDown(KeyCode.B))
            ToggleLights();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            m_IsTriggerOccupied = true;
            canvas.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            m_IsTriggerOccupied = false;
            canvas.enabled = false;
        }
    }

    public override bool CanInterAct()
    {
        return m_IsTriggerOccupied;
    }
    public override void ObjectInterAct()
    {
        Invoke("ToggleLights", m_Delay);
    }



    private void ToggleLights()
    {
        if (m_Barrier.enabled == false) m_TrafficManger.ToggleLights();
    }

    /// <summary>
    /// The below code is for testing Only to be removed after testing is complete.
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("mouse clicked");
        if (CanInterAct())
        {
            ObjectInterAct();
            Debug.Log("Lights Pressed");
        }
    }
    private void OnMouseEnter()
    {
        m_IsTriggerOccupied = true;
    }
    private void OnMouseExit()
    {
        m_IsTriggerOccupied = false;
    }

}
