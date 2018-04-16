using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public abstract class ActivatableObject : MonoBehaviour {


        /// <summary>
        /// Activatable is the prototype class for all objects that can be 'activated' by a player
        /// </summary>
        // Use this for initialization

        public SpawnManager m_SpawnManager;
        

        public abstract void Activate(GameObject other);
        public abstract void Activate();

        public void Reset()
        {
            Debug.Log("Resetting " + gameObject.ToString());
            m_SpawnManager.Spawn();
            Destroy(gameObject);
            
        }

    }
}