using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Система событий

// Класс, содержащий перечень кастомных объектов событий, каждый из которых содержит в себе
// событие и методы подписки/отписки/вызова. Классы этих кастомных событий идут далее в этом файле.
public static class Events
{
    // Player input
    public static readonly Evt<Card> onCardClicked = new Evt<Card>();

    // Tweening
    public static readonly Evt onTweensStarted = new Evt();
    public static readonly Evt onTweensEnded = new Evt();

    // GamePlay
    public static readonly Evt onWrongAnswer = new Evt();
    public static readonly Evt onRightAnswer = new Evt();
    public static readonly Evt onLevelSequenceComplete = new Evt();
    public static readonly Evt onLevelLoaded = new Evt();
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
