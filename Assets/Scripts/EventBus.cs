using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Oyun içi olaylarý tanýmlayan enum.
/// </summary>
public enum EventType
{
    ScoreChanged, Moved, GoalCountChanged, MoveCountOver, AllGoalCompleted, BoosterAndSwapsOverInLevel, BoosterUsed,
    LifeCountChanged, StarCountChanged
}

/// <summary>
/// Oyun içindeki olaylarý yönetmek için kullanýlan EventBus sýnýfý. Olaylarý dinleyici ekleme, kaldýrma ve yayma iþlevlerini saðlar.
/// </summary>
public class EventBus
{
    private readonly static IDictionary<EventType, UnityEvent> Events = new Dictionary<EventType, UnityEvent>();

    /// <summary>
    /// Belirli bir olay türü için dinleyici ekler.
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
    /// Belirli bir olay türü için dinleyiciyi kaldýrýr.
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
    /// Belirli bir olay türü için tüm dinleyicileri temizler.
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
    /// Belirli bir olay türünü yayar ve tüm dinleyicileri tetikler.
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
/// Oyun içindeki olaylarý yönetmek için kullanýlan EventBus sýnýfýnýn generic versiyonu. Tek bir parametre ile olaylarý yönetir.
/// </summary>
/// <typeparam name="T0">Olay parametresi türü</typeparam>
public class EventBus<T0>
{
    private readonly static IDictionary<EventType, UnityEvent<T0>> Events = new Dictionary<EventType, UnityEvent<T0>>();

    /// <summary>
    /// Belirli bir olay türü için dinleyici ekler.
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
    /// Belirli bir olay türü için dinleyiciyi kaldýrýr.
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
    /// Belirli bir olay türünü yayar ve tüm dinleyicileri tetikler.
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
/// Oyun içindeki olaylarý yönetmek için kullanýlan EventBus sýnýfýnýn generic versiyonu. Ýki parametre ile olaylarý yönetir.
/// </summary>
/// <typeparam name="T0">Birinci olay parametresi türü</typeparam>
/// <typeparam name="T1">Ýkinci olay parametresi türü</typeparam>
public class EventBus<T0, T1>
{
    private readonly static IDictionary<EventType, UnityEvent<T0, T1>> Events = new Dictionary<EventType, UnityEvent<T0, T1>>();

    /// <summary>
    /// Belirli bir olay türü için dinleyici ekler.
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
    /// Belirli bir olay türü için dinleyiciyi kaldýrýr.
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
    /// Belirli bir olay türünü yayar ve tüm dinleyicileri tetikler.
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
