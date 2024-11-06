using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Oyun i�i olaylar� tan�mlayan enum.
/// </summary>
public enum EventType
{
    ScoreChanged, Moved, GoalCountChanged, MoveCountOver, AllGoalCompleted, BoosterUsed,
    LifeCountChanged, StarCountChanged, AnyBoosterCountChanged
}

/// <summary>
/// Oyun i�indeki olaylar� y�netmek i�in kullan�lan EventBus s�n�f�. Olaylar� dinleyici ekleme, kald�rma ve yayma i�levlerini sa�lar.
/// </summary>
public class EventBus
{
    private readonly static IDictionary<EventType, UnityEvent> Events = new Dictionary<EventType, UnityEvent>();

    /// <summary>
    /// Belirli bir olay t�r� i�in dinleyici ekler.
    /// </summary>
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

    /// <summary>
    /// Belirli bir olay t�r� i�in dinleyiciyi kald�r�r.
    /// </summary>
    public static void Unsubscribe(EventType type, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Belirli bir olay t�r� i�in t�m dinleyicileri temizler.
    /// </summary>
    public static void Clear(EventType type)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveAllListeners();
        }
    }

    /// <summary>
    /// Belirli bir olay t�r�n� yayar ve t�m dinleyicileri tetikler.
    /// </summary>
    public static void Publish(EventType type)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}

/// <summary>
/// Oyun i�indeki olaylar� y�netmek i�in kullan�lan EventBus s�n�f�n�n generic versiyonu. Tek bir parametre ile olaylar� y�netir.
/// </summary>
/// <typeparam name="T0">Olay parametresi t�r�</typeparam>
public class EventBus<T0>
{
    private readonly static IDictionary<EventType, UnityEvent<T0>> Events = new Dictionary<EventType, UnityEvent<T0>>();

    /// <summary>
    /// Belirli bir olay t�r� i�in dinleyici ekler.
    /// </summary>
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

    /// <summary>
    /// Belirli bir olay t�r� i�in dinleyiciyi kald�r�r.
    /// </summary>
    public static void Unsubscribe(EventType type, UnityAction<T0> listener)
    {
        UnityEvent<T0> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Belirli bir olay t�r�n� yayar ve t�m dinleyicileri tetikler.
    /// </summary>
    public static void Publish(EventType type, T0 arg0)
    {
        UnityEvent<T0> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke(arg0);
        }
    }
}

/// <summary>
/// Oyun i�indeki olaylar� y�netmek i�in kullan�lan EventBus s�n�f�n�n generic versiyonu. �ki parametre ile olaylar� y�netir.
/// </summary>
/// <typeparam name="T0">Birinci olay parametresi t�r�</typeparam>
/// <typeparam name="T1">�kinci olay parametresi t�r�</typeparam>
public class EventBus<T0, T1>
{
    private readonly static IDictionary<EventType, UnityEvent<T0, T1>> Events = new Dictionary<EventType, UnityEvent<T0, T1>>();

    /// <summary>
    /// Belirli bir olay t�r� i�in dinleyici ekler.
    /// </summary>
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

    /// <summary>
    /// Belirli bir olay t�r� i�in dinleyiciyi kald�r�r.
    /// </summary>
    public static void Unsubscribe(EventType type, UnityAction<T0, T1> listener)
    {
        UnityEvent<T0, T1> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Belirli bir olay t�r�n� yayar ve t�m dinleyicileri tetikler.
    /// </summary>
    public static void Publish(EventType type, T0 arg0, T1 arg1)
    {
        UnityEvent<T0, T1> thisEvent;
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke(arg0, arg1);
        }
    }
}
