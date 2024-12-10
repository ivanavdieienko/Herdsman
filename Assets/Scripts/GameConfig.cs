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
    
    [Header("Spawn Settings")]
    [SerializeField]
    private int animalPoolSize = 10;
    [SerializeField]
    private float animalSpawnInterval = 5f;
    [SerializeField]
    private float yardOffset = -2.5f;

    public float HeroSpeed => heroSpeed;
    public int MaxGroupSize => maxGroupSize;
    public int AnimalPoolSize => animalPoolSize;
    public float AnimalFollowSpeed => animalFollowSpeed;
    public float AnimalCaptureRadius => animalCaptureRadius;
    public float AnimalSpawnInterval => animalSpawnInterval;
    public float YardOffset => yardOffset;
}
