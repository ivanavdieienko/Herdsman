using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Hero Settings")]
    [SerializeField]
    private float heroSpeed = 5f;
    [SerializeField]
    private int maxGroupSize = 5;
    [SerializeField]
    private float animalCaptureRadius = 1f;

    [Header("Animal Settings")]
    
    [SerializeField]
    private float animalFollowSpeed = 3f;
    [SerializeField]
    private float animalPatrolSpeed = 1f;
    [SerializeField]
    private float animalPatrolRange = 1f;
    [SerializeField]
    private float animalPatrolOffset = 0.5f;

    [Header("Spawn Settings")]
    [SerializeField]
    private int animalPoolSize = 10;
    [SerializeField]
    private float maxSpawnInterval = 5f;

    public float HeroSpeed => heroSpeed;
    public int MaxGroupSize => maxGroupSize;
    public int AnimalPoolSize => animalPoolSize;
    public float AnimalFollowSpeed => animalFollowSpeed;
    public float AnimalPatrolSpeed => animalPatrolSpeed;
    public float AnimalPatrolOffset => animalPatrolOffset;
    public float AnimalCaptureRadius => animalCaptureRadius;
    public float MaxSpawnInterval => maxSpawnInterval;
}
