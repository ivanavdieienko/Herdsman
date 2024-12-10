using System;
using UnityEngine;

public class FollowingState : IAnimalState
{
    private float _followOffset;
    private float _followSpeed;
    private Transform _followTarget;

    public FollowingState(Transform followTarget, float followOffset, float followSpeed)
    {
        _followTarget = followTarget;
        _followOffset = followOffset;
        _followSpeed = followSpeed;
    }

    public void EnterState(Animal animal)
    {
        if (_followTarget == null)
        {
            throw new ArgumentNullException(nameof(_followTarget));
        }
    }

    public void UpdateState(Animal animal)
    {
        Vector2 direction = (animal.transform.position - _followTarget.position).normalized;
        Vector2 targetPosition = (Vector2)_followTarget.position + direction * _followOffset;

        animal.transform.position = Vector2.MoveTowards(
            animal.transform.position,
            targetPosition,
            _followSpeed * Time.deltaTime
        );
    }

    public void HandleCollision(Collider2D collision, Animal animal)
    {
        if (collision.CompareTag(Tag.Yard))
        {
            animal.AnimalDelivered();
        }
    }

    public void ExitState(Animal animal)
    {
        
    }
}
