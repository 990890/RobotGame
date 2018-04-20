using Complete;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    #region Fields

    // number of tanks inside the spawnpoint
    private int _nObjects = 0;

    // list of tanks inside the spawnpoint
    private List<TankMovement> robots; 
    #endregion

    #region Properties

    private int nObjects {
        get {
            return _nObjects;
        }
        set {
            _nObjects = value;

            // if there are no objects the spawn point is no longer occupied
            Occupied = _nObjects <= 0 ? false : true;
        }

    }
    public bool Occupied { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        // init list
        robots = new List<TankMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TankMovement robot = other.GetComponent<TankMovement>();

        // add the robot to the list and increment the number of objects
        if (robot != null && robots.Contains(robot) == false) {
            robots.Add(robot);
            nObjects++;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        TankMovement robot = other.GetComponent<TankMovement>();

        // remove the robot to the list and decrement the number of objects
        if (robot != null && robots.Contains(robot)) {
            robots.Remove(robot);
            nObjects--;

        }
    }

    #endregion

}
