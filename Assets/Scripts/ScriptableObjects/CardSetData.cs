using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SO с одной коллекцией карточек

[CreateAssetMenu(fileName = "New CardSetData", menuName = "ScriptableObjects/CardSetData", order = 10)]
public class CardSetData : ScriptableObject
{
    [SerializeField] private CardData[] cards;

    public CardData[] Cards => cards;
}
