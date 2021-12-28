using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITweener : MonoBehaviour
{


    public void FadeIn()
    {
        Fade(0f);
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
}
