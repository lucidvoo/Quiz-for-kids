using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellTweener : MonoBehaviour
{
    [SerializeField] SpriteRenderer contentSprRen;
    [SerializeField] SpriteRenderer borderSprRen;
    [SerializeField] SpriteRenderer backgroundSprRen;
    [SerializeField] Transform gridCenter;

    private float fadeDuration = 1f;
    private float appearElasticDuration = 1f;
    private float shakeDuration = 1f;
    private float rightAnswerDuration = 2f;
    private float rightAnswerScaleMultiply = 3f;

    private Tweener transformTweener, contentTweener, borderTweener, backgrTweener, contentTweenerAdditional;

    private static int tweensInPlay = 0;


    private void Start()
    {
        // сохранить все начальные значения, которые будут меняться, для возможности последующего восстановления
    }

    public void AppearElastic()
    {
        KillAllTweeners();

        transformTweener = transform.DOScale(Vector3.zero, appearElasticDuration).From().SetEase(Ease.InElastic, 1.3f, 0.2f);
        transformTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }

    public void WrongShake()
    {
        KillAllTweeners();

        contentTweener = contentSprRen.transform.DOShakePosition(shakeDuration, new Vector3(1f, 0.3f) /* настроить другие параметры? */);
        //contentTweener.SetEase(Ease.OutQuad); добавить если будет слишком резко кончаться
        contentTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }

    public void RightAnswer()
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
    }


    private void OnTweenComplete()
    {
        tweensInPlay--;

        if (tweensInPlay == 0)
        {
            Events.onTweensEnded.Invoke();
        }
    }


    private void OnTweenStart()
    {
        tweensInPlay++;

        if (tweensInPlay == 1)
        {
            Events.onTweensStarted.Invoke();
        }
    }


    public void RestoreAppearance()
    {
        KillAllTweeners();


    }
}
