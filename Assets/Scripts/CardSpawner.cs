using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Transform topMiddleSpawnPos;
    [SerializeField] private GameObject cardPrefab;

    private Vector3 cardSize;

    private void Start()
    {
        Collider2D collider = cardPrefab.GetComponent<Collider2D>();
        cardSize = collider.bounds.size;

        Debug.Log("Размер коллайдера префаба карты: x = " + cardSize.x + ", y = " + cardSize.y);
    }


    public Card[,] SpawnCards(int rows, int cols, Transform parent)
    {
        Card[,] cards = new Card[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            float posY = topMiddleSpawnPos.position.y - r * cardSize.y;
            for (int c = 0; c < cols; c++)
            {
                float posX = topMiddleSpawnPos.position.x + (c - (cols - 1) * 0.5f) * cardSize.x;
                cards[r, c] = Instantiate(cardPrefab, new Vector3(posX, posY), cardPrefab.transform.rotation, parent).GetComponent<Card>();
            }
        }

        return cards;
    }
}
