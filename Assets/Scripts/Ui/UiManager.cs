using TMPro;
using UnityEngine;
using Zenject;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private UserData _userData;

    [Inject]
    private void Construct(UserData userData)
    {
        _userData = userData;
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
