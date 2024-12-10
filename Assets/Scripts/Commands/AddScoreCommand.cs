using Zenject;

public class AddScoreCommand
{
    [Inject]
    private UserData userData;

    public void Execute()
    {
        userData.AddScore();
    }
}