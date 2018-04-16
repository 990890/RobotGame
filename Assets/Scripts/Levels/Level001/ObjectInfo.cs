using UnityEngine;

/// <summary>
/// public class to hold the objects in the object pool and thier inital transfrom position 
/// </summary> 
public class ObjectInfo
{
    // The GameObject
    public GameObject m_Object;
    // The Game object's inital loction in the object pool.
    public Vector3 m_InitalPos;

    // Contructor to assign the objects and pool location.
    public ObjectInfo(GameObject obj, Vector3 vec)
    {
        SetObject(obj);
        SetPos(vec);
    }
    // Set Start pos
    public void SetPos(Vector3 vec)
    {
        m_InitalPos = vec;
    }
    // Set GameObject
    public void SetObject(GameObject Obj = null)
    {
        m_Object = Obj;
    }
}
