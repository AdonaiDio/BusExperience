using System;
using UnityEngine;
using UnityEngine.Events;
using PassengerList = System.Collections.Generic.List<Passenger>; //alias da classe genérica "List<Passenger>"
public static class Events{

    //public static readonly Evt onPlayerBusyEventExit = new Evt();
    //public static readonly Evt<MonoBehaviour> onDialogueEventExit = new Evt<MonoBehaviour>();

    public static readonly Evt<Transform> atDestinationEvent = new Evt<Transform>();
    public static readonly Evt<Passenger> RemovePassangerFromBusEvent = new Evt<Passenger>();
    public static readonly Evt<Passenger> AddPassangerFromBusEvent = new Evt<Passenger>();
    public static readonly Evt<PassengerList, PassengerList> OnEmbarkButtonEvent = new Evt<PassengerList, PassengerList>();
    public static readonly Evt CashInEvent = new Evt();
    public static readonly Evt<int> StartToMoveBusEvent = new Evt<int>();
}
public class Evt
{
    private event Action _action = delegate { };

    public void Invoke() => _action.Invoke();
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}
public class Evt<T>
{
    private event Action<T> _action = delegate { };

    public void Invoke(T param) => _action.Invoke(param);
    public void AddListener(Action<T> listener) => _action += listener;
    public void RemoveListener(Action<T> listener) => _action -= listener;
}

public class Evt<T0, T1>
{
    private event Action<T0, T1> _action = delegate { };

    public void Invoke(T0 param1, T1 param2) => _action.Invoke(param1, param2);
    public void AddListener(Action<T0, T1> listener) => _action += listener;
    public void RemoveListener(Action<T0, T1> listener) => _action -= listener;
}