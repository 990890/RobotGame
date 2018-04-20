using Complete;
using UnityEngine;

public class RobotPickable : MonoPickable {

    #region Fields

    // the force this robot is thrown
    [SerializeField]
    private float deployForce = 650;

    // non-trigger collider of the robot
    [SerializeField]
    private Collider rigidCollider;

    private TankMovement movement;
    private Rigidbody rb;
    
    // reference to this robot grabber
    private Grabber myGrabber;
    
    // reference to the picker robot grabber
    private Grabber otherGrabber;

    #endregion

    #region Methods

    private void Awake()
    {
        // init references
        movement = GetComponentInParent<TankMovement>();
        rb = GetComponentInParent<Rigidbody>();
        myGrabber = GetComponentInChildren<Grabber>();
    }
    public override void PickUp(Grabber picker)
    {
        base.PickUp(picker);
        AttachToPicker(picker);
        otherGrabber = picker;
        
    }
    public override void Deploy()
    {
        base.Deploy();
        DeattachFromPicker();
        ApplyForce();
    }
    private void AttachToPicker(Grabber picker) {
        // parenting the robot to the picker 
        transform.SetParent(picker.transform);

        // resetting local position and rotation
        transform.localPosition = new Vector3(0, -1, 1);
        transform.rotation = new Quaternion(0, 0, 0, 0);

        // disabling physics and movement control
        rb.isKinematic = true;
        rigidCollider.enabled = false;
        movement.enabled = false;
        rb.isKinematic = true;

        // disabling grab control
        myGrabber.IsDisabled = true;
    }
    private void DeattachFromPicker() {
        
        // dereferencing this object from the picker
        otherGrabber.Pickable = null;

        // deparenting this robot from the picker
        transform.parent = null;

        // enabling physics and movement control
        rigidCollider.enabled = true;
        movement.enabled = true;
        IsPickedUp = false;
        rb.isKinematic = false;

        // enabling grab control
        myGrabber.IsDisabled = false;

    }
    private void ApplyForce() {

        // throw this robot forward
        rb.AddForce(new Vector3(transform.forward.x, 1, transform.forward.z) * deployForce, ForceMode.Impulse);
    }
    public override void Despawn()
    {
        DeattachFromPicker();
        RobotSpawner.Instance.RespawnAtRandomSpawnPoint(transform.root);
    }
    #endregion


}
