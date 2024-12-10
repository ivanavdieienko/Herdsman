using UnityEngine;

public class PatrollingState : IAnimalState
{
    private float _patrolSpeed;
    private Rect _patrolArea;
    private Rect _yardArea;
    private Vector2 _patrolTarget;

    public PatrollingState(float patrolSpeed, Rect patrolArea, Rect yardArea)
    {
        _patrolSpeed = patrolSpeed;
        _patrolArea = patrolArea;
        _yardArea = yardArea;
    }

    public void EnterState(Animal animal)
    {
        SetNewPatrolTarget();
    }

    public void UpdateState(Animal animal)
    {
        animal.transform.position = Vector2.MoveTowards(
            animal.transform.position,
            _patrolTarget,
            _patrolSpeed * Time.deltaTime
        );

        if (Vector2.Distance(animal.transform.position, _patrolTarget) < 0.1f)
        {
            SetNewPatrolTarget();
        }
    }

    public void HandleCollision(Collider2D collision, Animal animal)
    {
        if (collision.CompareTag(Tag.Player)) //animal detected by player
        {
            animal.AnimalDetected();
        }
    }

    public void ExitState(Animal animal)
    {
    }

    private void SetNewPatrolTarget()
    {
        do
        {
            _patrolTarget = new Vector2(
                Random.Range(_patrolArea.xMin, _patrolArea.xMax),
                Random.Range(_patrolArea.yMin, _patrolArea.yMax)
            );
        }
        while (_yardArea.Contains(_patrolTarget));
    }
}
