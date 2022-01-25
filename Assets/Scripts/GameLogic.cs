using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private BoolVar isGameInPlay;
    [SerializeField] private CardGrid cardGrid;

    private bool isRightAnswerGiven = false;
    private int tweensInPlay = 0;



    private void OnEnable()
    {
        Events.onCardClicked.AddListener(OnCardClicked_Handler);
        Events.onTweenStarted.AddListener(OnTweenStarted_Handler);
        Events.onTweenEnded.AddListener(OnTweenEnded_Handler);
    }


    private void OnDisable()
    {
        Events.onCardClicked.RemoveListener(OnCardClicked_Handler);
        Events.onTweenStarted.RemoveListener(OnTweenStarted_Handler);
        Events.onTweenEnded.RemoveListener(OnTweenEnded_Handler);
    }


    private void OnCardClicked_Handler(Card card)
    {
        if (!isGameInPlay.value)
        {
            return;
        }

        CheckAnswer(card);
    }

    private void OnTweenStarted_Handler()
    {
        tweensInPlay++;

        //Debug.Log("tweens in play = " + tweensInPlay);

        if (tweensInPlay == 1)
        {
            BlockGameplay();
        }
    }


    private void OnTweenEnded_Handler()
    {
        tweensInPlay--;

        Debug.Log("tweens in play = " + tweensInPlay);

        if (tweensInPlay == 0)
        {
            UnBlockGameplay();

            CheckLevelCompletionCondition();
        }
    }


    private void CheckAnswer(Card cardClicked)
    {
        if (cardClicked.ContentIdentifier == cardGrid.RightAnswer)
        {
            // ответ верный
            Events.onRightAnswer.Invoke();

            cardClicked.RightAnswerTween(cardGrid.Spawner.SpawnPos);
            foreach (Card card in cardGrid.Cards)
            {
                if (card == cardClicked)
                {
                    continue;
                }
                card.FadeOut();
            }

            isRightAnswerGiven = true;
        }
        else
        {
            // ответ неверный
            Events.onWrongAnswer.Invoke();

            cardClicked.WrongAnswerTween();
        }
    }


    private void BlockGameplay() => isGameInPlay.SetTemporary(false);


    private void UnBlockGameplay() => isGameInPlay.Restore();


    private void CheckLevelCompletionCondition()
    {
        if (isRightAnswerGiven)
        {
            isRightAnswerGiven = false;
            cardGrid.GoToNextLevel();
        }
    }


}
