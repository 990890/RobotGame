using UnityEngine;
using System.Linq;

public class RobotSpawner : MonoBehaviour {

    #region Static 

    public static RobotSpawner Instance { get; private set; }

    #endregion

    #region Fields

    [SerializeField]
    private SpawnPoint[] spawnPoints;

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
        SpawnPoint spawnPoint = spawnPoints[index];

        if (spawnPoint.Occupied)
            spawnPoint = GetFirstFreeSpawnPoint();
        
        // set target position and rotation as the spawn point chosen
        target.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

        // if the target is holding a box it get despawned
        Box box = target.GetComponentInChildren<Box>();

        if (box != null)
            box.Despawn();
    }
    public SpawnPoint GetFirstFreeSpawnPoint() {
        return spawnPoints.Where(x => x.Occupied == false).FirstOrDefault();
    }
    public void RespawnAtRandomSpawnPoint(Transform target) {
        // get a random spawn point
        int index = Random.Range(0, spawnPoints.Length - 1);

        RespawnAtSpawnPoint(target,index);
    }

    #endregion
}
