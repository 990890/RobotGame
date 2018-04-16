using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class DeathZone : MonoBehaviour {

        public GameManager GameManager;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player")
            {
                int playerNumber = collision.collider.gameObject.GetComponent<TankMovement>().m_PlayerNumber;
                Debug.Log(string.Format("Player {0} fell off the edge", playerNumber));
                //TODO add a respawn timer here
                GameManager.m_Tanks[playerNumber - 1].Reset();
                
            }
            if (collision.collider.tag == "Pickup")
            {

                collision.collider.gameObject.GetComponent<ActivatableObject>().Reset();
            }

        }
    }
}
