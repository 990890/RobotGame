using UnityEngine;

public class RobotSpawner : MonoBehaviour {

    #region Static 

    public static RobotSpawner Instance { get; private set; }

    #endregion

    #region Fields

    [SerializeField]
    private Transform[] spawnPoints;

    #endregion

    #region Methods

    private void Awake()
    {
        
        // singleton ---- you just need one robot spawner per scene
        if (Instance == null)
            Instance = this;
        else {
            Debug.LogError("More than one RobotSpawner found in scene");
        }

        // spawn each player in a random spawn point at the start of the game
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            RespawnAtRandomSpawnPoint(item.transform);
        }

    }

    public void RespawnAtRandomSpawnPoint(Transform target) {

        // get a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];

        // set target position and rotation as the spawn point chosen
        target.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        // if the target is holding a box it get despawned
        Box box = target.GetComponentInChildren<Box>();

        if (box != null)
            box.Despawn();
    }

    #endregion
}
