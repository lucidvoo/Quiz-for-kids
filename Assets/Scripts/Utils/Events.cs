using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Класс, содержащий перечень кастомных объектов событий, каждый из которых содержит в себе
// событие и методы подписки/отписки/вызова. Классы этих кастомных событий идут далее в этом файле.
public static class Events
{
    // Примеры описания
    //public static readonly Evt onSmthHappened = new Evt();
    //public static readonly Evt<int> onSmthHappenedWithArg = new Evt<int>();

}

// событие без параметров
public class Evt
{
    private event Action EventAction = delegate { };

    public void AddListener(Action listenerMethod)
    {
        EventAction += listenerMethod;
    }

    public void RemoveListener(Action listenerMethod)
    {
        EventAction -= listenerMethod;
    }

    public void Invoke()
    {
        EventAction.Invoke();
    }
}

// событие с одним параметром
public class Evt<T>
{
    private event Action<T> EventAction = delegate { };

    public void AddListener(Action<T> listenerMethod)
    {
        EventAction += listenerMethod;
    }

    public void RemoveListener(Action<T> listenerMethod)
    {
        EventAction -= listenerMethod;
    }

    public void Invoke(T param)
    {
        EventAction.Invoke(param);
    }
}

public class Evt<T, V>
{
    private event Action<T, V> EventAction = delegate { };

    public void AddListener(Action<T, V> listenerMethod)
    {
        EventAction += listenerMethod;
    }

    public void RemoveListener(Action<T, V> listenerMethod)
    {
        EventAction -= listenerMethod;
    }

    public void Invoke(T param1, V param2)
    {
        EventAction.Invoke(param1, param2);
    }
}
