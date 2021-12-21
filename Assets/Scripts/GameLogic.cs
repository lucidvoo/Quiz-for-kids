using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private BoolVar isGameInPlay;
    [SerializeField] private CardGrid cardGrid;

    private bool isRightAnswerGiven = false;


    private void OnEnable()
    {
        Events.onCardClicked.AddListener(OnCardClicked_Handler);
        Events.onTweensStarted.AddListener(BlockGameplay);
        Events.onTweensEnded.AddListener(UnBlockGameplay);
    }


    private void OnDisable()
    {
        Events.onCardClicked.RemoveListener(OnCardClicked_Handler);
        Events.onTweensStarted.RemoveListener(BlockGameplay);
        Events.onTweensEnded.RemoveListener(UnBlockGameplay);
    }


    private void OnCardClicked_Handler(Card card)
    {
        if (!isGameInPlay.value)
        {
            return;
        }

        CheckAnswer(card);
    }


    private void CheckAnswer(Card card)
    {
        if (card.ContentIdentifier == cardGrid.RightAnswer)
        {
            // ответ верный
            Events.onRightAnswer.Invoke();

            card.RightAnswerTween(cardGrid.Spawner.SpawnPos);

            isRightAnswerGiven = true;
        }
        else
        {
            // ответ неверный
            Events.onWrongAnswer.Invoke();

            card.WrongAnswerTween();
        }
    }


    private void BlockGameplay() => isGameInPlay.SetTemporary(false);


    private void UnBlockGameplay()
    {
        isGameInPlay.Restore();

        if (isRightAnswerGiven)
        {
            cardGrid.GoToNextLevel();
        }
    }


    
}
