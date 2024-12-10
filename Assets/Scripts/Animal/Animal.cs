using UnityEngine;
using Zenject;

public class Animal : MonoBehaviour
{
    private IAnimalState _currentState;
    private IAnimalState _patrollingState; //cached state

    private Pool _pool;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(Pool pool, SignalBus signalBus)
    {
        _pool = pool;
        _signalBus = signalBus;
    }

    public void Initialize(float patrolSpeed, Rect patrolArea, Rect yardArea)
    {
        _patrollingState = new PatrollingState(patrolSpeed, patrolArea, yardArea);

        ChangeState(_patrollingState);
    }

    public void AnimalDetected()
    {
        _signalBus.Fire(new AnimalDetectedSignal { Animal = this });
    }

    public void AnimalDelivered()
    {
        _signalBus.Fire(new AnimalDeliveredSignal { Animal = this });
        _pool.Despawn(this);
    }

    public void StartFollowing(Transform target, float offset, float followSpeed)
    {
        ChangeState(new FollowingState(target, offset, followSpeed));
    }

    public void StopFollowing()
    {
        ChangeState(_patrollingState);
    }

    private void ChangeState(IAnimalState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentState.HandleCollision(collision, this);
    }


    public class Pool : MonoMemoryPool<Vector3, Animal>
    {
        protected override void Reinitialize(Vector3 position, Animal animal)
        {
            animal.transform.position = position;
            animal.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Animal animal)
        {
            animal.StopFollowing();
            animal.gameObject.SetActive(false);
        }
    }
}
