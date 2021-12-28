using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellTweener : MonoBehaviour
{
    [SerializeField] SpriteRenderer contentSprRen;
    [SerializeField] SpriteRenderer borderSprRen;
    [SerializeField] SpriteRenderer backgroundSprRen;

    private float fadeDuration = 1f;
    private float appearElasticDuration = 1f;
    private float shakeDuration = 0.8f;
    private float rightAnswerDuration = 2f;
    private float rightAnswerScaleMultiply = 2.5f;

    private InitialValues initialValues;

    private Tweener transformTweener, contentTweener, borderTweener, backgrTweener, contentTweenerAdditional;

    private class InitialValues
    {
        public Vector3 cardPos, cardScale;
        public Vector3 contentPos, contentScale;

        public InitialValues(Transform cardTransform, Transform contentTransform)
        {
            cardPos = cardTransform.position;
            cardScale = cardTransform.localScale;
            contentPos = contentTransform.localPosition;
            contentScale = contentTransform.localScale;
        }
    }

    private void Start()
    {
        // сохранить все начальные значения, которые будут меняться, для возможности последующего восстановления
        initialValues = new InitialValues(transform, contentSprRen.transform);
    }

    public void AppearElastic()
    {
        KillAllTweeners();

        transformTweener = transform.DOScale(Vector3.zero, appearElasticDuration).From().SetEase(Ease.OutElastic, 1.2f, 0.7f);
        transformTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }

    public void WrongShake()
    {
        KillAllTweeners();

        contentTweener = contentSprRen.transform.DOShakePosition(shakeDuration, new Vector3(0.15f, 0.02f));
        contentTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }

    public void RightAnswer(Transform gridCenter)
    {
        KillAllTweeners();

        Vector3 targetScale = contentSprRen.transform.localScale * rightAnswerScaleMultiply;
        contentTweener = contentSprRen.transform.DOScale(targetScale, rightAnswerDuration).SetEase(Ease.InOutCubic);
        contentTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);

        contentTweenerAdditional = contentSprRen.transform.DOMove(gridCenter.position, rightAnswerDuration).SetEase(Ease.InOutCubic);
        contentTweenerAdditional.OnStart(OnTweenStart).OnKill(OnTweenComplete);

        backgrTweener = borderSprRen.DOFade(0, fadeDuration);
        backgrTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);

        borderTweener = backgroundSprRen.DOFade(0, fadeDuration);
        borderTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }

    public void FadeOut()
    {
        Fade(0f);
    }

    private void Fade(float outOrInFloat)
    {
        KillAllTweeners();

        contentTweener = contentSprRen.DOFade(outOrInFloat, fadeDuration);
        contentTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);

        backgrTweener = borderSprRen.DOFade(outOrInFloat, fadeDuration);
        backgrTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);

        borderTweener = backgroundSprRen.DOFade(outOrInFloat, fadeDuration);
        borderTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }

    private void KillAllTweeners()
    {
        contentTweener?.Kill();
        borderTweener?.Kill();
        backgrTweener?.Kill();
        transformTweener?.Kill();
        contentTweenerAdditional?.Kill();
    }


    private void OnTweenComplete() => Events.onTweenEnded.Invoke();


    private void OnTweenStart() => Events.onTweenStarted.Invoke();


    public void RestoreAppearance()
    {
        KillAllTweeners();

        transform.position = initialValues.cardPos;
        transform.localScale = initialValues.cardScale;

        contentSprRen.transform.localPosition = initialValues.contentPos;
        contentSprRen.transform.localScale = initialValues.contentScale;

        contentSprRen.color = new Color(contentSprRen.color.r, contentSprRen.color.g, contentSprRen.color.b, 1f);
        borderSprRen.color = new Color(borderSprRen.color.r, borderSprRen.color.g, borderSprRen.color.b, 1f);
        backgroundSprRen.color = new Color(backgroundSprRen.color.r, backgroundSprRen.color.g, backgroundSprRen.color.b, 1f);
    }
}
