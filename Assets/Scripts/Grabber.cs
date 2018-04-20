using Complete;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Grabber : MonoBehaviour {

    #region Fields
    private bool skipInputCall;
    #endregion

    #region Properties

    public bool IsDisabled { get; set; }
    public int PlayerNumber { get; set; }
    public IPickable Pickable { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        PlayerNumber = GetComponentInParent<TankMovement>().m_PlayerNumber;
    }
    

    private void OnTriggerStay(Collider other)
    {
        // cannot bring more than one box 
        if (Pickable !=null || IsDisabled)
            return;

        if (Input.GetButtonDown(("PickupDeploy") + PlayerNumber)) {
            IPickable otherPickable = other.GetComponent<IPickable>();

            // it is not a pickable object 
            if (otherPickable == null)
                return;
            
            // pickable object has already been picked up
            if (otherPickable.IsPickedUp)
                return;

            // caching pickable object
            Pickable = otherPickable;
            
            Pickable.PickUp(this);
            skipInputCall = true;
        }
    }

    private void Update()
    {
        if (IsDisabled)
            return;

        if (!skipInputCall) {

            // input call 
            if (Input.GetButtonDown(("PickupDeploy") + PlayerNumber) && Pickable != null)
            {
                DeployPickable();
            }
        }

        skipInputCall = false;

    }

    public void DeployPickable() {
        Pickable.Deploy();
        Pickable = null;
    }
    public void DespawnPickable() {
        if(Pickable!=null)
            Pickable.Despawn();
    }
    #endregion

}
