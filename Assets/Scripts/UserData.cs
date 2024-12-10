using System;

public class UserData
{
    public event Action<int> OnScoreChanged;

    private int _score;

    public int Score => _score;

    public void AddScore()
    {
        _score++;
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        OnScoreChanged?.Invoke(_score);
    }
}
