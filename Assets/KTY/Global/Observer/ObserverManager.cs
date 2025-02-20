using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ObserverManager : MonoBehaviour
{
    private List<Observer> observers = new();
    public void Subcribe(Observer observer)
    {
        observers.Add(observer);
    }

    public void UnSubcribe(Observer observer)
    {
        observers.Remove(observer);
    }

    public void Noitfy()
    {
        foreach (Observer observer in observers)
        {
            observer.Update();
        }
    }

}


