using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Vector2 cardSize;

    private Card[,] cards;

    public Transform SpawnPos => spawnPos;


    public Card[,] SpawnCards(int rows, int cols, Transform parent)
    {
        if (cards != null)
        {
            RemoveCards();
        }
        
        cards = new Card[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            float posY = SpawnPos.position.y - (r - (rows - 1) * 0.5f) * cardSize.y;
            for (int c = 0; c < cols; c++)
            {
                float posX = SpawnPos.position.x + (c - (cols - 1) * 0.5f) * cardSize.x;
                cards[r, c] = Instantiate(cardPrefab, new Vector3(posX, posY), cardPrefab.transform.rotation, parent);
            }
        }

        return cards;
    }

    public void RemoveCards()
    {
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }

        cards = null;
    }
}
