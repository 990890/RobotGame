using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive : MonoBehaviour
{

    public abstract bool CanInterAct();
    public abstract void ObjectInterAct();
}