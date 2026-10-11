using CSE_325_Team_16_Eunomia.Models;

namespace CSE_325_Team_16_Eunomia.Services;

public class UserSessionService
{
    public UserAccount? CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser != null;

    public event Action? OnChange;

    public void SetUser(UserAccount user)
    {
        CurrentUser = user;
        NotifyStateChanged();
    }

    public void ClearUser()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

