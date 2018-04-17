using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoxGrabber : MonoBehaviour {

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        // cannot bring more than one box 
        if (transform.GetComponentsInChildren<Box>().Length > 0)
            return;

        Box box = other.GetComponent<Box>();

        // it is not a box 
        if (box == null)
            return;

        box.PickUp(transform);
    }

    #endregion

}
