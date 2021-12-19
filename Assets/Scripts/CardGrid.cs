using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGrid : MonoBehaviour
{

    [SerializeField] private CardSetData[] cardSets;
    [SerializeField] private LevelSequenceData levelSequence;
    [SerializeField] private CardSpawner spawner;
    [SerializeField] private BoolVar isGameInPlay;

    // key - индекс использованного набора карточек, value - набор использованных правильных ответов
    private Dictionary<int, HashSet<string>> usedRightAnswers = new Dictionary<int, HashSet<string>>(2);
    private int currentLevelInd = 0;
    private int currentCardSetInd;
    private string rightAnswer;
    private Card[,] cards;

    void Start()
    {
        isGameInPlay.value = true;

        SetupGrid();
    }


    // Подготовка поля к игре и запуск уровня
    private void SetupGrid()
    {
        cards = spawner.SpawnCards(levelSequence.Levels[currentLevelInd].LevelSizeRows, 
                                   levelSequence.Levels[currentLevelInd].LevelSizeColumns, 
                                   transform);

        SetupCardContents();
        PickRightAnswer();
    }


    // выбор случайного набора контента, выставление случайных значений из набора в ячейки.
    private void SetupCardContents()
    {
        // случайно выбрать набор карточек
        currentCardSetInd = UnityEngine.Random.Range(0, cardSets.Length);

        // случайно выбрать массив карточек из набора, которые мы используем в уровне
        if (cardSets[currentCardSetInd].Cards.Length < cards.Length)
        {
            Debug.LogError("Один из наборов карточек слишком мал. Добавьте элементы или уменьшите размер уровня");
        }

        List<CardData> fullCardSet = new List<CardData>(cardSets[currentCardSetInd].Cards.Length);
        for (int i = 0; i < cardSets[currentCardSetInd].Cards.Length; i++)
        {
            fullCardSet.Add(cardSets[currentCardSetInd].Cards[i]);
        }

        List<CardData> selectedCardSet = new List<CardData>(cards.Length);
        for (int i = 0; i < cards.Length; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, fullCardSet.Count);
            selectedCardSet.Add(fullCardSet[randIndex]);
            fullCardSet.RemoveAt(randIndex);
        }
        fullCardSet = null; // название больше не отражает содержимого массива

        // записать найденный массив в ячейки
        int ind = 0;
        foreach (Card card in cards)
        {
            card.SetContent(selectedCardSet[ind]);
            ind++;
        }
    }


    // из рабочего массива карточек случайно принять одну за верное решение с проверкой, чтобы такой не было ранее
    private void PickRightAnswer()
    {
        if (!usedRightAnswers.ContainsKey(currentCardSetInd))
        {
            usedRightAnswers.Add(currentCardSetInd, new HashSet<string>());
        }
        else
        {
            string identifierToCheck;
            do
            {
                int r = UnityEngine.Random.Range(0, cards.GetLength(0));
                int c = UnityEngine.Random.Range(0, cards.GetLength(1));
                identifierToCheck = cards[r, c].ContentIdentifier;
            } while (usedRightAnswers[currentCardSetInd].Contains(identifierToCheck));

            rightAnswer = identifierToCheck;
            usedRightAnswers[currentCardSetInd].Add(rightAnswer);
        }
    }
}
