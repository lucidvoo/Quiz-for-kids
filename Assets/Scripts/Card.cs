using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer contentSpriteRenderer;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private CellTweener cellTweener;

    private string contentIdentifier;

    public string ContentIdentifier => contentIdentifier;

    public void SetContent(CardData cardData)
    {
        this.contentIdentifier = cardData.Identifier;
        contentSpriteRenderer.sprite = cardData.CardSprite;

        SetContentProperSize();
    }


    // изменить скейл спрайта контента так, чтобы он вписался в указанный на префабе прямоугольник
    private void SetContentProperSize()
    {
        contentSpriteRenderer.transform.localScale = Vector3.one;

        // определить аспект-ратио спрайта и ректа
        float spriteAspect = contentSpriteRenderer.bounds.size.x / contentSpriteRenderer.bounds.size.y;
        float rectAspect = contentRect.rect.width / contentRect.rect.height;
        float scaleToSet;

        // посчитать нужный скейл по ширине, либо по высоте
        if (spriteAspect > rectAspect)
        {
            scaleToSet = (contentRect.rect.width) / contentSpriteRenderer.bounds.size.x;
        }
        else
        {
            scaleToSet = contentRect.rect.height / contentSpriteRenderer.bounds.size.y;
        }

        // назначить скейл
        contentSpriteRenderer.transform.localScale *= scaleToSet;
    }


    private void OnMouseDown()
    {
        Events.onCardClicked.Invoke(this);
    }

    public void WrongAnswerTween()
    {
        cellTweener.WrongShake();
    }

    public void RightAnswerTween(Transform gridCenter)
    {
        cellTweener.RightAnswer(gridCenter);
    }
    
    public void AppearTween()
    {
        cellTweener.AppearElastic();
    }

    public void FadeOut()
    {
        cellTweener.FadeOut();
    }
}
