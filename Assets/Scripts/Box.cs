using System;
using UnityEngine;

public class Box : MonoBehaviour {

    #region Fields
    
    public event Action OnDespawn = delegate { };
    public event Action OnPickUp = delegate { };
    #endregion

    #region Properties

    public bool PickedUp { get; private set; }

    #endregion

    #region Methods


    public void PickUp(Transform picker) {

        // it can only be picked up once
        if (PickedUp)
            return;

        PickedUp = true;

        // parenting the box to the picker (player's object picker) and resetting its local position
        transform.SetParent(picker);
        transform.localPosition = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);

        // calling any events connected to this box pickup
        OnPickUp();

    }
    public void Despawn()
    {
        /* Despawn get called every time a box is deployed or the player respawn */

        // calling any events connected to this box pickup
        OnDespawn();
        Destroy(gameObject);
    }
    public void Deploy() {
        transform.parent = null;
        PickedUp = false;
    }
    #endregion

}
