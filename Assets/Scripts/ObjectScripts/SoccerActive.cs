using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class SoccerActive : ActivatableObject
    {

        public float m_LaunchPower = 15f;

        // Use this for initialization
        void Start()
        {
            return;
        }

        public override void Activate(GameObject other)
        {
            //gameObject method call
            Debug.Log("Soccer Ball was kicked!");

            this.gameObject.GetComponent<Rigidbody>().velocity = m_LaunchPower * other.GetComponent<Transform>().forward;
            if (other.GetComponent<Complete.PlayerPickup>().m_HeldObject == this.gameObject)
            {
                other.GetComponent<Complete.PlayerPickup>().Drop();
            }
        }
        public override void Activate()
        {
            Debug.Log("Wrong form of Activate called, requires an argument! Called from " + this.gameObject);
        }
    }
}