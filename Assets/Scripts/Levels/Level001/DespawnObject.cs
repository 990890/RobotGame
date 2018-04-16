using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class moves the objects that have been moved off screen back to the object pool
/// </summary>
/// 


public class DespawnObject : MonoBehaviour {

    // the obejct to despawns tag.
    public string m_ObjectTag = "MovingObjects";
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {
        // get the collider of the object if it tag is of the type in m_Objects
        if (col.gameObject.tag == m_ObjectTag)
        {
            // get the controller componet;
            CarController cc = col.GetComponent<CarController>();
            // Set the rigid body's gravity to false
            col.GetComponent<Rigidbody>().useGravity = false;
            // set the objects state to IDEL a
            cc.SetState(CARSTATE.DESPAWNING);
        }
    }

}
