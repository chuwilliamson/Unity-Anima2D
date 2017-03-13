
using UnityEngine.Events;
public class EventPlayer : UnityEvent
{
}

public static class Events {
    public static EventPlayer OnPlayerDied = new EventPlayer();
    public static EventPlayer OnPlayerLose = new EventPlayer();
    public static EventPlayer OnPlayerWin = new EventPlayer();
    public static EventPlayer OnPlayerLevelUp = new EventPlayer();
}
