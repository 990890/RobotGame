using UnityEngine;

/* BASE CLASS FOR EVERY PICKABLE OBJECT */
public abstract class MonoPickable : MonoBehaviour, IPickable
{


    #region Properties

    public bool IsPickedUp { get; set; }

    #endregion

    #region Methods

    public virtual void Deploy()
    {
        IsPickedUp = false;
        
    }

    public virtual void PickUp(Grabber picker)
    {
        // cannot be picked up more than once
        if (IsPickedUp)
            return;

        IsPickedUp = true;
    }

    public abstract void Despawn();

    #endregion



}
