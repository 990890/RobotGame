using UnityEngine;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public float m_Speed = 12f;                 // How fast the tank moves forward and back.
        public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
        public float m_Accel = 3f;                  // The factor by which horizontal and vertical shifts occur
        public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
        public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
		public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
        public float m_minimumInput = 0.1f;         // The cutoff value under which no movement will be recorded
        
        private string m_VerticalAxisName;          // The name of the input axis for moving forward and back.
        private string m_HorizontalAxisName;              // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.
        private float m_VerticalInputValue;         // The current value of the movement input.
        private float m_MovementAccelValue;         // The current value of the acceleration
        private float m_TurnAccelValue;         // The current value of the turning
        private float m_HorizontalInputValue;             // The current value of the turn input.
        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks
        private float m_InputAngle;                // Equal to the angle of the player's joystick
        private float m_CameraOffset;               // The rotation of the camera by which all player's movements are skewed
        private string m_BreakButton;               // When held, prevents player movement

        private void Awake ()
        {
            m_Rigidbody = GetComponent<Rigidbody> ();
            
        }


        private void OnEnable ()
        {
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // Also reset the input values.
            m_VerticalInputValue = 0f;
            m_HorizontalInputValue = 0f;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Play();
            }

            // 
            try
            {
                m_CameraOffset = FindObjectOfType<Camera>().GetComponentInParent<Transform>().eulerAngles.y;
            }
            catch
            {
                Debug.Log("No Camera Found!");
            }
        }


        private void OnDisable ()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for(int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
            }
        }


        private void Start ()
        {
            // The axes names are based on player number.
            m_VerticalAxisName = "Vertical" + m_PlayerNumber;
            m_HorizontalAxisName = "Horizontal" + m_PlayerNumber;
            m_BreakButton = "Break" + m_PlayerNumber;

            // Store the original pitch of the audio source.
            m_OriginalPitch = m_MovementAudio.pitch;
        }


        private void Update ()
        {
            // Store the value of both input axes.
            m_VerticalInputValue = Input.GetAxis (m_VerticalAxisName);
            m_HorizontalInputValue = Input.GetAxis (m_HorizontalAxisName) * -1;
            m_InputAngle = (Mathf.Atan2(m_VerticalInputValue, m_HorizontalInputValue) * Mathf.Rad2Deg) + m_CameraOffset - 90f;

            float detectedMovement = Mathf.Max(Mathf.Abs(m_VerticalInputValue),Mathf.Abs(m_HorizontalInputValue));

            m_TurnAccelValue = 0;
            if (detectedMovement > m_minimumInput) //if input is detected, apply movements
            {
                if (Input.GetButton(m_BreakButton))
                {
                    m_MovementAccelValue +=  (2 * m_Accel * -(Mathf.Sign(m_MovementAccelValue))) * Time.deltaTime;
                    if ((m_MovementAccelValue) < m_minimumInput)
                    {
                        m_MovementAccelValue = 0;
                    }
                }
                else
                {
                    m_MovementAccelValue += (m_Accel * detectedMovement) * Time.deltaTime;
                    m_MovementAccelValue = Mathf.Clamp(m_MovementAccelValue, 0, detectedMovement);
                }

                float deltaAngle = Mathf.DeltaAngle(m_Rigidbody.transform.eulerAngles.y, m_InputAngle);
                if (Mathf.Abs(deltaAngle) > 6) // This piece of code will adjust the factor by which the player should rotate to face the desired direction
                    m_TurnAccelValue = Mathf.Sign(deltaAngle);
                else if (Mathf.Abs(deltaAngle) > 1) // If the player is within 5 degrees of the right direction, it snaps to the desired direction
                    m_TurnAccelValue = Mathf.Sign(deltaAngle)/m_Speed;

                
            }
            else //decrease movement
            {
                m_MovementAccelValue += (m_Accel * -(Mathf.Sign(m_MovementAccelValue))) * Time.deltaTime;
                if (Mathf.Abs(m_MovementAccelValue) < m_minimumInput)
                {
                    m_MovementAccelValue = 0;
                }
            }


            //DEBUG
            if (m_PlayerNumber == 3)
            {
                //Debug.Log(m_TurnAccelValue);

            }





            EngineAudio();
        }


        private void EngineAudio ()
        {
            // If there is no input (the tank is stationary)...
            if (Mathf.Abs (m_VerticalInputValue) < 0.1f && Mathf.Abs (m_HorizontalInputValue) < 0.1f)
            {
                // ... and if the audio source is currently playing the driving clip...
                if (m_MovementAudio.clip == m_EngineDriving)
                {
                    // ... change the clip to idling and play it.
                    m_MovementAudio.clip = m_EngineIdling;
                    m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play ();
                }
            }
            else
            {
                // Otherwise if the tank is moving and if the idling clip is currently playing...
                if (m_MovementAudio.clip == m_EngineIdling)
                {
                    // ... change the clip to driving and play.
                    m_MovementAudio.clip = m_EngineDriving;
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play();
                }
            }
        }


        private void FixedUpdate ()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move ();
            Turn ();
        }


        private void Move ()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_MovementAccelValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }


        private void Turn ()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames
            float turn = m_TurnAccelValue * m_TurnSpeed * Time.deltaTime;

           
            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
            

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
           
        }
    }
}