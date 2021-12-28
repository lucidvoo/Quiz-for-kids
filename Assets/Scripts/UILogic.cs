using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILogic : MonoBehaviour
{
    [SerializeField] private CardGrid cardGrid;
    [SerializeField] private BoolVar isGameInPlay;
    [SerializeField] private TMP_Text findText;
    [SerializeField] private Image restartPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Image loadingPanel;
    [SerializeField] private UITweener tweener;


    private void Start()
    {
        restartPanel.gameObject.SetActive(false);
        loadingPanel.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        Events.onLevelLoaded.AddListener(SetFindText);
        Events.onLevelSequenceComplete.AddListener(ShowRestartScreen);
        Events.onGameStarted.AddListener(OnGameStarted_Handler);
    }


    private void OnDisable()
    {
        Events.onLevelLoaded.RemoveListener(SetFindText);
        Events.onLevelSequenceComplete.RemoveListener(ShowRestartScreen);
        Events.onGameStarted.RemoveListener(OnGameStarted_Handler);
    }


    private void SetFindText() => findText.text = "Find " + cardGrid.RightAnswer;


    private void OnGameStarted_Handler() => tweener.TweenFindText();


    private void ShowRestartScreen()
    {
        isGameInPlay.value = false;
        tweener.TweenRestart();
    }


    public void RestartButtonHandler()
    {
        tweener.HideRestart();
        
        tweener.TweenInLoadingScreen();

        // перезагрузка уровня
        // плавное исчезновение загрузочного
        // после исчезновения запуск всех твинов и только после этого разблокирование уравления игроку.
    }
}
