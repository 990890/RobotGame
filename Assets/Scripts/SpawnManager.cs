using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class SpawnManager : MonoBehaviour
    {

        public GameObject m_SpawnObject;

        

        // Use this for initialization
        void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            GameObject Object = Instantiate(m_SpawnObject, gameObject.transform);
            Object.GetComponentInChildren<ActivatableObject>().m_SpawnManager = this;
        }
    }
}