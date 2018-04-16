using System.Collections.Generic;
using UnityEngine;
/// <Info>
/// This class and struct are manaing the pool of objects that will be aviliable to the spawners
/// </Info>
/// 

public class ObjectPool : MonoBehaviour
{

    private static List<ObjectInfo> m_Objects = new List<ObjectInfo>();

    //**********************************************************************//
    // Unity Callback methods
    //**********************************************************************//

    // Use this for initialization
    void Awake()
    {
        //get an array of rigidbodys (ie object that we want to move)
        Rigidbody[] rig = GetComponentsInChildren<Rigidbody>();
        // incriment value to populate m_Objects Array
        int i = 0;
        // loop throght rig array getting the intial position for each object.
        foreach (Rigidbody obj in rig)
        {
            // Initalize the array element and assign GameObject and 
            m_Objects.Add(new ObjectInfo(obj.gameObject, obj.transform.position));
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //**********************************************************************//
    // Custom methods
    //**********************************************************************//

    // Pablic method  that take a game Object and returns the intial position to  
    // the depawner so it can return the object to the pool out side the camera view.

    public Vector3 ObjectPosInPool(GameObject obj)
    {
        // loop through array until gameObject matches else return an out of camera pos
        foreach (ObjectInfo OBJ in m_Objects)
        {
            if (OBJ.m_Object == obj)
                return OBJ.m_InitalPos;
        }
        //out of camera pos -10 in the y
        return new Vector3(0, -10, 0);
    }

    public List<ObjectInfo> GetPool()
    {
        return m_Objects;
    }

    public Vector3 PoolPostition(GameObject obj)
    {

        //loop through to get the initial Pos
        foreach (ObjectInfo Obj in m_Objects)
        {
            if (Obj.m_Object.name == obj.name) return Obj.m_InitalPos;
           
        }
        // return out of way location if loop fails
        return new Vector3(0,-10,0);

    }
}