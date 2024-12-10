using UnityEngine;
using Zenject;

public class AnimalSpawner : ITickable
{
    private GameConfig _config;
    private Animal.Pool _animalMemoryPool;
    private float _spawnTimer;
    private Rect _cachedBounds;


    [Inject]
    public void Initialize(GameConfig config, Animal.Pool animalMemoryPool)
    {
        _config = config;
        _animalMemoryPool = animalMemoryPool;

        var mainCamera = Camera.main;

        var screenHeight = mainCamera.orthographicSize * 2;
        var screenWidth = screenHeight * mainCamera.aspect;

        _cachedBounds = new Rect( -screenWidth / 2, -screenHeight / 2, screenWidth - 2, screenHeight - 2 + _config.YardOffset);
    }

    public void Tick()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _config.AnimalSpawnInterval)
        {
            SpawnAnimal();
            _spawnTimer = 0f;
        }
    }

    private void SpawnAnimal()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(_cachedBounds.xMin, _cachedBounds.xMax),
            Random.Range(_cachedBounds.yMin, _cachedBounds.yMax),
            0f
        );

        _animalMemoryPool.Spawn(randomPosition);
    }
}
