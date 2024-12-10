using UnityEngine;

public interface IAnimalState
{
    void EnterState(Animal animal);
    void UpdateState(Animal animal);
    void HandleCollision(Collider2D collision, Animal animal);
    void ExitState(Animal animal);
}
