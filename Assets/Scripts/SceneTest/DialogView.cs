using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogView : MonoBehaviour
{
    public event Action OnFinishMessage; ///< Ивент при вызове финального сообщения в диалоге

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    private DialogPresenter _dialogPresenter;
    /*! Присваивание переменной говорящего NPC*/
    public void SetPresenter(DialogPresenter presenter)
    {
        _dialogPresenter = presenter;
    }
    /*! Старт диалога*/
    public void StartDialogue(string message, string name)
    {
        _nameText.text = name;
        NewMessage(message);
    }
    /*! Остановка диалога*/
    public void StopDialogue()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        HideDialog();
    }

    private void HideDialog()
    {
        _nameText.text = "";
        _messageText.text = "";
    }
    /*! Следующие сообщение в диалоговой ветке*/
    public void NextMessage(string message, string name)
    {
        _nameText.text = name;
        NewMessage(message);
    }

    private void NewMessage(string text)
    {
        _messageText.text = text;
    }
    /*! Сокрытие кнопок выбора*/
    public void HideButtons()
    {
        foreach (var button in _buttons)
            button.SetActive(false);
    }
    /*! Активация кнопок выбора*/
    public void ActivateButtons(List<string> shortNames)
    {
        for (int i = 0; i < shortNames.Count; i++)
        {
            _buttons[i].SetActive(true);
            _buttonsText[i].text = shortNames[i];
        }
    }
    /*! Финальное сообщение в ветке*/
    private void FinishMessage()
    {
        OnFinishMessage?.Invoke();
    }
    /*! Функция вызывающаяся после нажатия кнопки выбора*/
    public void ClickOnButtonChoice(int indexButton)
    {
        HideButtons();
        _dialogPresenter.SwitchBranch(indexButton);
    }
}
