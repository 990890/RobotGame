using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Boundary : MonoBehaviour {

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        // robots cannot throw stuff while in the boundary

        // has entered boundary
        Grabber grabber = other.GetComponentInChildren<Grabber>();
        if (grabber != null)
            grabber.IsDisabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        // robots cannot throw stuff while in the boundary

        // has exited boundary
        Grabber grabber = other.GetComponentInChildren<Grabber>();
        if (grabber != null)
            grabber.IsDisabled = false;
    }
    #endregion
}
