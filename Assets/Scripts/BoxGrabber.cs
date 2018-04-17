using Complete;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoxGrabber : MonoBehaviour {

    #region Fields

    private TankMovement playerPickup;
    private Box box;
    private bool skipInputCall;
    #endregion

    #region Methods

    private void Awake()
    {
        playerPickup = GetComponentInParent<TankMovement>();
    }
    

    private void OnTriggerStay(Collider other)
    {
        // cannot bring more than one box 
        if (box !=null)
            return;

        if (Input.GetButtonDown(("PickupDeploy") + playerPickup.m_PlayerNumber)) {

            Box otherBox = other.GetComponent<Box>();

            // it is not a box 
            if (otherBox == null)
                return;

            // box is already picked up
            if (otherBox.PickedUp)
                return;

            box = otherBox;
            box.PickUp(transform);
            skipInputCall = true;
        }
    }

    private void Update()
    {
        if (!skipInputCall) {
            if (Input.GetButtonDown(("PickupDeploy") + playerPickup.m_PlayerNumber) && box != null)
            {
                box.Deploy();
                box = null;
            }
        }
        skipInputCall = false;

    }
    #endregion

}
