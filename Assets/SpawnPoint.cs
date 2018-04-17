using Complete;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    #region Fields

    private int _nObjects = 0;

    #endregion

    #region Properties

    private int nObjects {
        get {
            return _nObjects;
        }
        set {
            _nObjects = value;
            Occupied = _nObjects == 0 ? false : true;
        }

    }
    public bool Occupied { get; private set; }

    #endregion

    #region Methods


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TankMovement>() != null)
            nObjects++;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TankMovement>() != null)
            nObjects--;
    }

    #endregion

}
