using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class UITweener : MonoBehaviour
{
    [SerializeField] private TMP_Text findText;
    [SerializeField] private Image restartPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Image loadingPanel;

    private float fadeTextDuration = 1f;
    private float fadeRestartBackgDuration = 1f;
    private float restartButtonTweenDuration = 1f;
    private float loadingScrTweenDuration = 1f;

    private Tweener findTextTweener, restartBackgTweener, restartButtonTweener, loadingScrTweener;

    public void TweenFindText()
    {
        // используется Fade In
        
        findTextTweener?.Kill();

        findTextTweener = findText.DOFade(0f, fadeTextDuration).From();
        findTextTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }
    

    public void TweenRestart()
    {
        // Fade In фона и после этого Elastic кнопки

        restartBackgTweener?.Kill();

        restartPanel.gameObject.SetActive(true);

        restartBackgTweener = restartPanel.DOFade(0f, fadeRestartBackgDuration).From();
        restartBackgTweener.OnStart(OnTweenStart).OnKill(OnRestartBackgTweenComplete);
    }

    public void OnRestartBackgTweenComplete()
    {
        restartButtonTweener?.Kill();

        restartButton.gameObject.SetActive(true);

        restartButtonTweener = restartButton.transform.DOScale(Vector3.zero, restartButtonTweenDuration).From();
        restartButtonTweener.SetEase(Ease.OutElastic, 1.2f, 0.7f);
        restartButtonTweener.OnKill(OnTweenComplete);
    }

    internal void HideRestart()
    {
        restartPanel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void TweenInLoadingScreen()
    {
        loadingScrTweener?.Kill();

        loadingPanel.gameObject.SetActive(true);

        loadingScrTweener = loadingPanel.DOFade(0f, loadingScrTweenDuration).From();
        loadingScrTweener.OnStart(OnTweenStart).OnKill(OnTweenComplete);
    }


    private void OnTweenComplete() => Events.onTweenEnded.Invoke();


    private void OnTweenStart() => Events.onTweenStarted.Invoke();
}
