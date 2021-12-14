using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Создание/удаление/подгонка контента карточки

[RequireComponent(typeof(RectTransform))]
public class CardContent : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private GameObject content;

    private void Awake()
    {
        //AdjustContentTransform();
    }

    //public void Set(GameObject contentPrefab)

    //public void Clear()

    //private void AdjustContentTransform()
}
