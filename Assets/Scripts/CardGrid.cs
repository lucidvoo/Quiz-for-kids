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

    void Start()
    {
        isGameInPlay.value = true;

        spawner.SpawnCards(levelSequence.Levels[currentLevelInd].LevelSizeRows, levelSequence.Levels[currentLevelInd].LevelSizeColumns, transform);
    }
}
