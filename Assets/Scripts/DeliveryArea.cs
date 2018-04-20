using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DeliveryArea : MonoBehaviour {
    
    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        BoxPickable box = other.GetComponent<BoxPickable>();
        
        // the object is a box and it gets delivered
        if (box != null)
            box.Delivery();
    }

    #endregion
}
