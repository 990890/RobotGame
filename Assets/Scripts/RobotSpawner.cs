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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            RespawnAtSpawnPoint(players[i].transform,i);

        }
       
    }
    public void RespawnAtSpawnPoint(Transform target, int index)
    {
        Transform spawnPoint = spawnPoints[index];

        // set target position and rotation as the spawn point chosen
        target.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        // if the target is holding a box it get despawned
        Box box = target.GetComponentInChildren<Box>();

        if (box != null)
            box.Despawn();
    }
    public void RespawnAtRandomSpawnPoint(Transform target) {
        // get a random spawn point
        int index = Random.Range(0, spawnPoints.Length - 1);

        RespawnAtSpawnPoint(target,index);
    }

    #endregion
}
