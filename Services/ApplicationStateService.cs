using WCRCorder.Models;

namespace WCRCorder.Services;

public class ApplicationStateService
{
    private readonly object _syncRoot = new();

    public ApplicationState CurrentState { get; private set; } = ApplicationState.Starting;

    public event Action<ApplicationState>? StateChanged;

    public void SetState(ApplicationState newState)
    {
        lock (_syncRoot)
        {
            if (CurrentState == newState)
                return;

            CurrentState = newState;
        }

        StateChanged?.Invoke(newState);
    }
}