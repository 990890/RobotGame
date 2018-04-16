using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPilot : MonoBehaviour {

    // follow distance
    [SerializeField]
    float m_FollowDistance = 5;

    [SerializeField]
    float m_RayDistance = 25;
    // stoping distance
    [SerializeField]
    float m_StoppingDistance = 2;
    [SerializeField]
    float m_BrakingCoeffcient = 1f;
    [SerializeField]
    float m_AccelerationCoeffcient = 1f;
    // speed limit. this comes from m_speed in Car Controller script.m
    private float m_Speed;
    // store the distance in the last frame to the game object infront.
    private float m_LastHitDistance;
    // The Carcontroller Componet.
    private CarController m_CarCtrl;

    // set the m_Speed when the car is spawned.
    public void SetSpeedLimit(float speed)
    {
        m_Speed = speed;
    }
    // this script runs the carcontroller making it speed up, slow down and stop.
    // cast a raycast to check traffic and trafice lights ahead.
    private bool CastRay()
    {
        // MovingObjects_Col
        // Bit shift the index of the layer (10) to get a bit mask
        int layerMask = 1 << 10;
        var velocity = m_CarCtrl.GetSpeed();

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, m_RayDistance, layerMask))
        {
            if (hit.distance  >= m_FollowDistance && m_Speed > velocity)
            {
                // if the car infront is accelorating increase speed to match.
                if (velocity <= m_Speed && velocity >= m_Speed * 0.25f) m_CarCtrl.SetSpeed(velocity + m_AccelerationCoeffcient * Time.deltaTime);
                else
                    m_CarCtrl.SetSpeed(velocity + m_AccelerationCoeffcient * Time.deltaTime * 0.25f);
            }
            if (hit.distance < m_LastHitDistance && m_Speed >= velocity)
            {
                if (velocity < m_Speed*0.3 && hit.distance > m_StoppingDistance)
                {
                    m_CarCtrl.SetSpeed(velocity);
                }
                // if the car infront is accelorating increase speed to match. 
                else if (velocity <= 0 || hit.distance < m_StoppingDistance )
                {
                    m_CarCtrl.SetSpeed(0);
                }
                else {
                    m_CarCtrl.SetSpeed(velocity -  m_BrakingCoeffcient * Time.deltaTime);
                }
            }
             
            m_LastHitDistance = hit.distance;
#if (UNITY_EDITOR) // draw a ray for the objects radar in the editor.
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
#endif
            // Debug.Log("Did Hit");

            return true;
        }
        else
        {

           
#if (UNITY_EDITOR)
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * m_RayDistance, Color.white);
#endif

            if (m_CarCtrl.GetSpeed() < m_Speed) m_CarCtrl.SetSpeed(m_CarCtrl.GetSpeed() + m_AccelerationCoeffcient * Time.deltaTime);
            else m_CarCtrl.SetSpeed(m_Speed);
            // Debug.Log("Did not Hit");
            return false;
        }
    }
    // if the car infront is slowing done
    // slow down car
    // if traffic lighs red or the car in front is stoped then stop at set distance.
    // if the traffic lighs green go.

    //**********************************************************************//
    // Unity Callback methods
    //**********************************************************************//

    // Use this for initialization
    void Start () {
        // set the car controller
        m_CarCtrl = gameObject.GetComponent<CarController>();
        m_Speed = m_CarCtrl.m_Speed;

    }

    // Update is called once per frame
    void Update () {
		
	}

    // fixed update for all physics Calulations
    private void FixedUpdate()
    {
        CastRay();
    }

    
}