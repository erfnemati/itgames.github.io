using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public enum EventName
{
    // UI button events
    OnLevelVictory,
    OnLevelDefeat,
    OnLevelRetreat,
    //
    OnGameWin, // all levels complete
    GameOver, // all lives lost
    
    OnTimeOver, // timer zero's
    // 
    OnColorAdded,  
    OnColorRemoved,
    OnBlitzHappened,
    //
    TheEnd,
    GameDuration
}



public class EventManager : IGameService
{

    private Dictionary<EventName, Delegate> eventDictionary;
    public Action<int, VectorInt> OnColorAdded;
    public Action<int, VectorInt> OnColorRemoved;
    public delegate void EventReceiver();
    public delegate void ColorEvent(int ShapeId, VectorInt color);

    public EventManager()
    {
        ServiceLocator._instance.Register(this);
        eventDictionary = new Dictionary<EventName, Delegate>();
    }
    public void StartListening(EventName eventName, Delegate listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = System.Delegate.Combine(eventDictionary[eventName], listener);
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }
    public  void StopListening(EventName eventName, System.Delegate listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = System.Delegate.Remove(eventDictionary[eventName], listener);
        }
    }
    public void TriggerEvent<T1,T2>(EventName eventName, T1 eventArg1,T2 eventArg2)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            var del = eventDictionary[eventName];

            if (del is System.Action<T1,T2> action)
            {
                action.Invoke(eventArg1,eventArg2);
            }
            else
            {
                Debug.LogError("Attempting to invoke event with incompatible delegate types. Check your event arguments.");
            }
        }
    }
    public void TriggerEvent(EventName eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            var del = eventDictionary[eventName];

            if (del is System.Action action)
            {
                action.Invoke();
            }
            else
            {
                Debug.LogError("Attempting to invoke event with incompatible delegate types. Check your event arguments.");
            }
        }
    }
    public void PreDestroy() { }
}
