using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// один элемент содержимого карточки. Спрайт и соответствующее текстовое обозначение.
[System.Serializable]
public class CardData
{
    [SerializeField] private string identifier;
    [SerializeField] private Sprite cardSprite;

    public string Identifier => identifier;
    public Sprite CardSprite => cardSprite;
}
