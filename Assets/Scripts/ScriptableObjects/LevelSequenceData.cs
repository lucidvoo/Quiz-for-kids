using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SO, позволяющий создать нужную последовательность уровней в редакторе

[CreateAssetMenu(fileName = "New level sequence", menuName = "ScriptableObjects/LevelSequence", order = 20)]
public class LevelSequenceData : ScriptableObject
{
    [SerializeField] private LevelData[] levels;

    public LevelData[] Levels => levels;
}
