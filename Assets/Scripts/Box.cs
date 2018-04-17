using System;
using UnityEngine;

public class Box : MonoBehaviour {

    #region Fields
    
    private bool pickedUp;
    public event Action OnDespawn = delegate { };
    public event Action OnPickUp = delegate { };
    #endregion

    #region Methods
    

    public void PickUp(Transform picker) {

        // it can only be picked up once
        if (pickedUp)
            return;

        pickedUp = true;

        // parenting the box to the picker (player's object picker) and resetting its local position
        transform.SetParent(picker);
        transform.localPosition = Vector3.zero;

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
        Despawn();
    }
    #endregion

}
