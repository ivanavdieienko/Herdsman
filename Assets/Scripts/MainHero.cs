using UnityEngine;
using Zenject;

public class MainHero : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D collider;

    private GameConfig _config;
    private Vector3 _targetPosition;

    [Inject]
    public void Construct(GameConfig config)
    {
        _config = config;

        collider.radius = _config.AnimalCaptureRadius;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetPosition.z = 0;
        }

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _config.HeroSpeed * Time.deltaTime);
    }
}
