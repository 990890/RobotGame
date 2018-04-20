using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CarController))]
public class CarCollisionDetector : MonoBehaviour {

    #region Fields

    private CarController controller;

    #endregion

    #region Methods

    private void Awake()
    {
        controller = GetComponent<CarController>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        // if the car is stopped, the player will not get killed
        if (Mathf.Abs(controller.GetSpeed()) == 0)
            return;

        // cache hit object transform
        Transform otherTransform = collision.collider.transform;

        // if the hit object is the player, respawn it
        if (otherTransform.CompareTag("Player"))
        {
            RobotSpawner.Instance.RespawnAtRandomSpawnPoint(otherTransform);
        }

        // if the hit object has a grabber, despawn its pickable
        Grabber grabber = otherTransform.GetComponentInChildren<Grabber>();
        if (grabber != null)
        {
            grabber.DespawnPickable();
        }
    }
    #endregion


}
