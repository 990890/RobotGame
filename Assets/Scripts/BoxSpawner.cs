using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    #region Fields

    [SerializeField]
    private float spawnDelay = 2f;

    [SerializeField]
    private Box boxPrefab;

    #endregion

    #region Methods

    private void Start()
    {
        GenerateBox();
    }

    public void DelayGenerateBox() {

        // call GenerateBox after a delay amount
        Invoke("GenerateBox", spawnDelay);
    }
    public void GenerateBox() {

        // cannot hold more than one box 
        if (transform.GetComponentsInChildren<Box>().Length > 0)
            return;

        // instantiate a new box 
        Box box = Instantiate(boxPrefab,transform.position,Quaternion.identity,transform);

        // add callbacks to the box events
        box.OnPickUp += DelayGenerateBox;
        box.OnDespawn += DelayGenerateBox;

    }

    #endregion

}
