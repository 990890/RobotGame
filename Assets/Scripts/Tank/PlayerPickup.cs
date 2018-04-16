using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class PlayerPickup : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Collider m_PickupCollider;           // The trigger zone for pickup items
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public GameObject m_HeldObject;             // The currently held object
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.
        


        private string m_FireButton;                // The input axis that is used for picking up items
        private string m_ActiveButton;              // The input axis used for activating world objects (buttons etc)
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool m_Held;                       // Whether or not an item is currently held



        private void OnEnable()
        {
            
            
        }


        private void Start ()
        {
            // The fire axis is based on the player number.
            m_FireButton = "Fire" + m_PlayerNumber;
            m_ActiveButton = "Active" + m_PlayerNumber;
            m_Held = false;
        }

        private void OnTriggerStay ( Collider other)
        {
            if (other.gameObject.tag == "Pickup" || (other.gameObject.tag == "Button"))
            {
                
                if (Input.GetButtonDown(m_FireButton) && other.gameObject.tag == "Pickup")
                {
                    Pickup(other.gameObject);
                    Debug.Log("Player " + m_PlayerNumber + " picked up an object.");
                }

                if (Input.GetButtonDown(m_ActiveButton))
                {
                    ActivatableObject activation = other.gameObject.GetComponent<ActivatableObject>();
                    if (activation != null)
                    {
                        activation.Activate(this.gameObject);
                    }
                }
            }
            
            
        }

        private void Update ()
        {
            

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_Held)
            {
                if (Input.GetButtonDown(m_ActiveButton))
                {
                    ActivatableObject activation = m_HeldObject.gameObject.GetComponent<ActivatableObject>();
                    if (activation != null)
                    {
                        activation.Activate(this.gameObject);
                    }
                }

                if (Input.GetButtonUp(m_FireButton))
                {
                    //drop object
                    Drop();
                    
                }

                // Error checking and object movement
                if (m_HeldObject != null)
                {
                    m_HeldObject.transform.SetPositionAndRotation(m_FireTransform.position,m_FireTransform.rotation);
                }
                else
                {
                    m_Held = false;
                }
            }
            // Otherwise, if the fire button has just started being pressed...
            
            /*
            else if (Input.GetButton (m_FireButton) && !m_Fired)
            {
                
            }
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetButtonUp (m_FireButton) && !m_Fired)
            {
                // ... launch the shell.
                Fire ();
            }*/
        }
        private void Pickup(GameObject pickUpObject)
        {
            m_Held = true;
            m_HeldObject = pickUpObject;
            m_HeldObject.GetComponent<Rigidbody>().isKinematic = true;
            m_HeldObject.GetComponent<Collider>().enabled = false;
        }
        public void Drop()
        {
            m_Held = false;
            if (m_HeldObject)
            {
                m_HeldObject.GetComponent<Rigidbody>().isKinematic = false;
                m_HeldObject.GetComponent<Collider>().enabled = true;
                m_HeldObject = null;
            }
            
        }

        private void Fire ()
        {
            // Set the fired flag so only Fire is only called once.
            

            // Create an instance of the shell and store a reference to it's rigidbody.
            //Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            //shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();

            // Reset the launch force.  This is a precaution in case of missing button events.
           
        }
    }
}