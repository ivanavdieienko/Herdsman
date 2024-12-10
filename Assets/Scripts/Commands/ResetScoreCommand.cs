using Zenject;

public class ResetScoreCommand
{
    [Inject]
    private UserData userData;

    public void Execute()
    {
        userData.ResetScore();
    }
}