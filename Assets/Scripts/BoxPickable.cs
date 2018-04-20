using System;
using UnityEngine;

public class BoxPickable : MonoPickable {

    #region Fields

    // non-trigger collider of the box
    [SerializeField]
    private Collider rigidCollider;

    private Rigidbody rb;

    // reference to the picker robot grabber
    private Grabber otherGrabber;

    // pickup callback
    public event Action OnPickUp = delegate { };

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void PickUp(Grabber picker)
    {

        base.PickUp(picker);

        AttachToPicker(picker);

        // calling any events connected to this box pickup
        OnPickUp();

    }
    public override void Despawn()
    {
        /* Despawn get called every time a box is deployed or the player respawn */
        Deploy();
        Destroy(gameObject);
    }

   
    public override void Deploy()
    {
        base.Deploy();
        DeattachFromPicker();
    }

    public void AttachToPicker(Grabber picker)
    {
        // parenting the box to the picker 
        transform.SetParent(picker.transform);

        // resetting local position and rotation
        transform.localPosition = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);

        // disabling physics
        rb.isKinematic = true;
        rigidCollider.enabled = false;

        // caching picker 
        otherGrabber = picker;
    }

    public void DeattachFromPicker()
    {
        // deparenting this box from the picker
        transform.parent = null;

        // enabling physics
        rb.isKinematic = false;
        rigidCollider.enabled = true;

        // dereferencing this object from the picker
        otherGrabber.Pickable = null;
    }

    public void Delivery() {
        ScoreManager.Instance.AddScore(otherGrabber.PlayerNumber);
        Despawn();
    }
    #endregion
}
