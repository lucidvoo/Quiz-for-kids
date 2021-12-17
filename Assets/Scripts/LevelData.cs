using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// хранилище для данных об уровне. В частности о количестве карточек в рядах и столбцах.

[System.Serializable]
public class LevelData
{
    [SerializeField] private int levelSizeRows;
    [SerializeField] private int levelSizeColumns;

    public int LevelSizeRows => levelSizeRows;
    public int LevelSizeColumns => levelSizeColumns;
}
