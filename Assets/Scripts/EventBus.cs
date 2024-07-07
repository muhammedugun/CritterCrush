using System.Collections.Generic;
using UnityEngine.Events;


public enum EventType
{
    ScoreChanged, Moved, GoalCountChanged, MoveCountOver, AllGoalCompleted
}


public class EventBus
{
    private static readonly IDictionary<EventType, UnityEvent> Events = new Dictionary<EventType, UnityEvent>();

    public static void Subscribe(EventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }
    public static void Unsubscribe(EventType type, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Clear(EventType type)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveAllListeners();
        }
    }

    public static void Publish(EventType type)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

}

public class EventBus<T0>
{
    private static readonly IDictionary<EventType, UnityEvent<T0>> Events = new Dictionary<EventType, UnityEvent<T0>>();

    public static void Subscribe(EventType eventType, UnityAction<T0> listener)
    {
        UnityEvent<T0> thisEvent;
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<T0>();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }
    public static void Unsubscribe(EventType type, UnityAction<T0> listener)
    {
        UnityEvent<T0> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void Publish(EventType type, T0 arg0)
    {
        UnityEvent<T0> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke(arg0);
        }
    }

}


public class EventBus<T0, T1>
{
    private static readonly IDictionary<EventType, UnityEvent<T0, T1>> Events = new Dictionary<EventType, UnityEvent<T0, T1>>();

    public static void Subscribe(EventType eventType, UnityAction<T0, T1> listener)
    {
        UnityEvent<T0, T1> thisEvent;
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<T0, T1>();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }
    public static void Unsubscribe(EventType type, UnityAction<T0, T1> listener)
    {
        UnityEvent<T0, T1> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void Publish(EventType type, T0 arg0, T1 arg1)
    {
        UnityEvent<T0, T1> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke(arg0, arg1);
        }
    }

}
