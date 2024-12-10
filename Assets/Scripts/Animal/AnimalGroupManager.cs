using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimalGroupManager : IInitializable, IDisposable
{
    private readonly List<Animal> _group = new();
    private const int MaxGroupSize = 5;

    private readonly GameConfig _config;
    private readonly SignalBus _signalBus;
    private readonly Transform _heroTransform;

    public AnimalGroupManager(SignalBus signalBus, MainHero heroController, GameConfig config)
    {
        _config = config;
        _signalBus = signalBus;
        _heroTransform = heroController.transform;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<AnimalDetectedSignal>(OnAnimalDetected);
        _signalBus.Subscribe<AnimalDeliveredSignal>(OnAnimalDelivered);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<AnimalDetectedSignal>(OnAnimalDetected);
        _signalBus.Unsubscribe<AnimalDeliveredSignal>(OnAnimalDelivered);
    }

    private void OnAnimalDetected(AnimalDetectedSignal signal)
    {
        if (_group.Count < MaxGroupSize && !_group.Contains(signal.Animal))
        {
            signal.Animal.StartFollowing(_heroTransform, _config.AnimalPatrolOffset, _config.AnimalFollowSpeed);
            _group.Add(signal.Animal);
        }
    }

    private void OnAnimalDelivered(AnimalDeliveredSignal signal)
    {
        if (_group.Contains(signal.Animal))
        {
            _group.Remove(signal.Animal);
        }
    }
}
