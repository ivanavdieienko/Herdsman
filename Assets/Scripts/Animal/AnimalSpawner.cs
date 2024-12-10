using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AnimalSpawner : ITickable
{
    private GameConfig _config;
    private Animal.Pool _animalMemoryPool;
    private float _spawnTimer;
    private Rect _animalsArea;
    private Rect _yardArea;


    [Inject]
    public void Initialize(GameConfig config, Animal.Pool animalMemoryPool)
    {
        _config = config;
        _animalMemoryPool = animalMemoryPool;

        UpdateGameAreas();

        _spawnTimer = _config.MaxSpawnInterval;
    }

    public void Tick()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _config.MaxSpawnInterval)
        {
            SpawnAnimal();
            _spawnTimer = Random.Range(0f, _config.MaxSpawnInterval - Time.deltaTime);
        }
    }

    public void UpdateGameAreas()
    {
        UpdateYardArea();

        var mainCamera = Camera.main;

        Vector2 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        var fullScreenArea = new Rect(screenMin, screenMax - screenMin);

        _animalsArea = new Rect(
            fullScreenArea.xMin,
            fullScreenArea.yMin + _yardArea.height,
            fullScreenArea.width,
            fullScreenArea.height - _yardArea.height
        );
    }

    public void UpdateYardArea()
    {
        var yard = GameObject.FindGameObjectWithTag(Tag.Yard);
        if (yard == null)
        {
            throw new NullReferenceException($"Can't find Object with tag {Tag.Yard}");
        }

        var yardRenderer = yard.GetComponent<Renderer>();
        if (yardRenderer == null)
        {
            throw new NullReferenceException($"Yard gameObject has no Renderer component");
        }

        Vector2 yardMin = yardRenderer.transform.position - yardRenderer.bounds.extents;
        Vector2 yardMax = yardRenderer.transform.position + yardRenderer.bounds.extents;

        _yardArea = new Rect(yardMin, yardMax - yardMin);
    }

    public void SpawnAnimal()
    {
        Vector2 spawnPosition;

        do
        {
            spawnPosition = new Vector2(
                Random.Range(_animalsArea.xMin, _animalsArea.xMax),
                Random.Range(_animalsArea.yMin, _animalsArea.yMax)
            );
        }
        while (_yardArea.Contains(spawnPosition));

        var animal = _animalMemoryPool.Spawn(spawnPosition);
        animal.Initialize(_config.AnimalPatrolSpeed, _animalsArea, _yardArea);
    }
}
