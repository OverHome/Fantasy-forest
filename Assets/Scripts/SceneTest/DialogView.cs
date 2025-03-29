using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogView : MonoBehaviour
{
    public event Action OnFinishMessage;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    private DialogPresenter _dialogPresenter;

    public void SetPresenter(DialogPresenter presenter)
    {
        _dialogPresenter = presenter;
    }

    public void StartDialogue(string message, string name)
    {
        _nameText.text = name;
        NewMessage(message);
    }

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

    public void NextMessage(string message, string name)
    {
        _nameText.text = name;
        NewMessage(message);
    }

    private void NewMessage(string text)
    {
        _messageText.text = text;
    }

    public void HideButtons()
    {
        foreach (var button in _buttons)
            button.SetActive(false);
    }
    public void ActivateButtons(List<string> shortNames)
    {
        for (int i = 0; i < shortNames.Count; i++)
        {
            _buttons[i].SetActive(true);
            _buttonsText[i].text = shortNames[i];
        }
    }

    private void FinishMessage()
    {
        OnFinishMessage?.Invoke();
    }
    public void ClickOnButtonChoice(int indexButton)
    {
        HideButtons();
        _dialogPresenter.SwitchBranch(indexButton);
    }
}
