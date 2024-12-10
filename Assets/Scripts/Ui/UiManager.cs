using TMPro;
using UnityEngine;
using Zenject;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private UserData _userData;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(UserData userData, SignalBus signalBus)
    {
        _userData = userData;
        _signalBus = signalBus;
    }

    private void Start()
    {
        _signalBus.Fire<ResetScoreSignal>();
    }

    private void OnEnable()
    {
        _userData.OnScoreChanged += UpdateScoreUI;
    }

    private void OnDisable()
    {
        _userData.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }
}
