public interface IPickable {

    bool IsPickedUp { get; set; }

    void PickUp(Grabber picker);
    void Deploy();
    void Despawn();

}
