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
    }


    // выбор случайного набора контента, выставление случайных значений из набора в ячейки, выбор правильного варианта.
    private void SetupCardContents()
    {
        // случайно выбрать набор карточек
        currentCardSetInd = UnityEngine.Random.Range(0, cardSets.Length);

        // случайно выбрать массив карточек, которые мы используем
        if (cardSets[currentCardSetInd].Cards.Length < cards.Length)
        {
            Debug.LogError("Один из наборов карточек слишком мал. Добавьте элементы или уменьшите размер уровня");
        }



        // из рабочего массива карточек случайно принять одну за верное решение с проверкой, чтобы такой не было ранее






        if (!usedRightAnswers.ContainsKey(currentCardSetInd))
        {
            usedRightAnswers.Add(currentCardSetInd, new HashSet<string>());
        }


    }
}
