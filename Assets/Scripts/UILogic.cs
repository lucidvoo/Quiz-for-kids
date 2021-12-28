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
    }

    private void OnDisable()
    {
        Events.onLevelLoaded.RemoveListener(SetFindText);
        Events.onLevelSequenceComplete.RemoveListener(ShowRestartScreen);
    }

    private void SetFindText()
    {
        findText.text = cardGrid.RightAnswer;

        // твинер фэйдина текста
    }

    private void ShowRestartScreen()
    {
        // заблокировать управление игроку
        // с твином вывести фон, после появления фона показать кнопку рестарт

    }

    public void RestartButtonHandler()
    {
        // плавное появление загрузочного
        // убрать рестарт-окошко
        // перезагрузка уровня
        // плавное исчезновение загрузочного
        // после исчезновения запуск всех твинов и только после этого разблокирование уравления игроку.
    }
}
