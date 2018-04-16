using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Car controller for level 1.
/// </summary>

public enum CARSTATE
{
    IDEL,
    SPAWNING,
    MOVING,
    DESPAWNING
};

public enum OBJECTDIRECTION
{
    MOVING_RIGHT = 1,
    MOVING_LEFT = -1,
    MOVING_UP = 2,
    MOVING_DOWN = -2,
};

public class CarController : MonoBehaviour
{
    // enum for cars state
    public CARSTATE m_CarState = CARSTATE.IDEL;
    // car direction
    public OBJECTDIRECTION m_CarDirection = OBJECTDIRECTION.MOVING_RIGHT;
    // speed
    public float m_Speed = 2;


    // the objects rigidbody
    private Rigidbody m_ObjectRigidbody;
    private ObjectPool m_ObjectPool;

    //**********************************************************************//
    // Unity Callback methods
    //**********************************************************************//

    // Use this for initialization
    void Start()
    {
        m_ObjectRigidbody = GetComponent<Rigidbody>();
        m_ObjectPool = FindObjectOfType<ObjectPool>().GetComponent<ObjectPool>();

    }


    // Update is called once per frame
    void Update()
    {
        if (m_CarState == CARSTATE.SPAWNING)
        {
            // add the int value of direction of travel to m_speed
            if (m_CarDirection == OBJECTDIRECTION.MOVING_LEFT && m_Speed > 0 )
                m_Speed = -1 * m_Speed;
            else if (m_CarDirection == OBJECTDIRECTION.MOVING_DOWN && m_Speed > 0)
                m_Speed = -1 * m_Speed;
            else if (m_CarDirection == OBJECTDIRECTION.MOVING_UP && m_Speed < 0)
                m_Speed = -1 * m_Speed;
            else if (m_CarDirection == OBJECTDIRECTION.MOVING_RIGHT && m_Speed < 0)
                m_Speed = -1 * m_Speed;
            RotateCar();
            m_CarState = CARSTATE.MOVING;


            if (gameObject.GetComponent<AutoPilot>() != null) gameObject.GetComponent<AutoPilot>().SetSpeedLimit(m_Speed);
            else Debug.LogWarning("AutoPilot script not attatched to " + gameObject.name);
        }
        else if (m_CarState == CARSTATE.MOVING)
        {
            //move the object
            MoveObject(m_Speed);
        }
        else if (m_CarState == CARSTATE.DESPAWNING)
        {
            // move it back to the pool.
            m_Speed = 0;
            MoveToPool();
        }
    }


    //**********************************************************************//
    // Custom methods
    //**********************************************************************//

    public void MoveObject(float speed)
    {
        // enable gravity
        m_ObjectRigidbody.useGravity = true;
        // set velocity
        if (m_CarDirection == OBJECTDIRECTION.MOVING_LEFT || m_CarDirection == OBJECTDIRECTION.MOVING_RIGHT)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (m_CarDirection == OBJECTDIRECTION.MOVING_UP || m_CarDirection == OBJECTDIRECTION.MOVING_DOWN)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y , transform.position.z + speed * Time.deltaTime);
        }

    }

    // public flot to return the object speed to calling scipts.
    public float GetSpeed ()
    {
        return m_Speed;
    }
    // outside scripts can change the speed.
    public void SetSpeed(float speed)
    {
        m_Speed = speed;
    }
    internal void SetState(CARSTATE state)
    {
        m_CarState = state;
    }

    private void MoveToPool()
    {
        // move it back to the pool.
        // Get the original position
        transform.position = m_ObjectPool.PoolPostition(gameObject);
        // Reset the Rotation
        m_CarDirection = OBJECTDIRECTION.MOVING_RIGHT;
        RotateCar();
        // change the state to idel
        m_CarState = CARSTATE.IDEL;
    }
    
    private void RotateCar()
    {
        // setup rotation based on object travel direction
        if (m_CarDirection == OBJECTDIRECTION.MOVING_LEFT)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (m_CarDirection == OBJECTDIRECTION.MOVING_RIGHT)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }        // setup rotation based on object travel direction
        if (m_CarDirection == OBJECTDIRECTION.MOVING_UP)
        {
            gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else if (m_CarDirection == OBJECTDIRECTION.MOVING_DOWN)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }

}
