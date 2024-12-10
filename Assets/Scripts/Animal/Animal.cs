using UnityEngine;
using Zenject;

public class Animal : MonoBehaviour
{
    private const string TagYard = "Yard";
    private const string TagPlayer = "Player";

    private Pool _pool;
    private GameConfig _config;
    private SignalBus _signalBus;
    private Transform _followTarget;
    private bool _isFollowing;

    [Inject]
    public void Construct(Pool animalPool, GameConfig config, SignalBus signalBus)
    {
        _pool = animalPool;
        _config = config;
        _signalBus = signalBus;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void StartFollowing(Transform target)
    {
        _isFollowing = true;
        _followTarget = target;
    }

    private void Update()
    {
        if (_isFollowing && _followTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _followTarget.position, _config.AnimalFollowSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagYard))
        {
            _signalBus.Fire(new AnimalDeliveredSignal { Animal = this });
            _pool.Despawn(this);
        }
        else if (collision.CompareTag(TagPlayer))
        {
            _signalBus.Fire(new AnimalDetectedSignal { Animal = this });
        }
    }

    public class Pool : MonoMemoryPool<Vector3, Animal>
    {
        protected override void Reinitialize(Vector3 position, Animal animal)
        {
            animal.Spawn(position);
        }

        protected override void OnDespawned(Animal animal)
        {
            animal.Despawn();
        }
    }
}