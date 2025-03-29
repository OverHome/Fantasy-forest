
using System; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

enum DialoguePresenterState
{
    Await,
    Talk,
    Choice,
    Finish
}

public class DialogPresenter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private DialogView _dialogueView;
    [SerializeField] private TextAsset[] _dialoguesArr;
    private uint _currentDialogue;
    private DialogNode _currentNode;
    public Button Start; 
    public Button Next;

    [Header("Events")]
    [SerializeField] private UnityEvent[] OnDialogueFinished;
    public event Action OnDialogueStart;

    [field: Header("States")]
    [field: SerializeField] public bool CanTalk { get; set; }
    [SerializeField] private DialoguePresenterState _state;


    public void StartDialogue()
    {
        if (_state == DialoguePresenterState.Finish || !CanTalk)
            return;

        _currentNode = ParseDialogFile.GetDialogTree(_dialoguesArr[_currentDialogue]);

        _dialogueView.SetPresenter(this);
        OnDialogueStart?.Invoke();
        CanTalk = false;
        _dialogueView.StartDialogue(_currentNode.Message, _currentNode.Name);
        _state = DialoguePresenterState.Talk;
        Start.gameObject.SetActive(false);
    }

    public void NextMessage()
    {
        if (_state != DialoguePresenterState.Talk || _state == DialoguePresenterState.Choice)
            return;

        if (_currentNode.Children.Count == 0)
        {
            FinishDialogue();
            return;
        }

        GoToChildMessage();
    }

    private void GoToChildMessage()
    {
        if (_currentNode.Children.Count == 1)
        {
            _currentNode = _currentNode.Children[0];
            _dialogueView.NextMessage(_currentNode.Message, _currentNode.Name);
            return;
        }

        List<string> shortNames = new List<string>();
        foreach (var child in _currentNode.Children)
            shortNames.Add(child.ShortName);
        _dialogueView.ActivateButtons(shortNames);
        _state = DialoguePresenterState.Choice;
    }

    private void FinishDialogue()
    {
        _dialogueView.StopDialogue();
        if (_currentNode.ActionId != null)
            OnDialogueFinished[(int)_currentNode.ActionId].Invoke();

        _state = DialoguePresenterState.Await;
        _currentDialogue++;
        if (_currentDialogue > _dialoguesArr.Length - 1)
        {
            _state = DialoguePresenterState.Finish;
        }
        Next.gameObject.SetActive(false);
    }

    public void SwitchBranch(int index)
    {
        Debug.Log("Button clicked " + index.ToString());
        _currentNode = _currentNode.Children[index];
        _state = DialoguePresenterState.Talk;
        _dialogueView.NextMessage(_currentNode.Message, _currentNode.Name);
    }

    public void Action0()
    {
        Debug.Log("Первая ветка закончена");
    }

    public void Action1()
    {
        Debug.Log("Вторая ветка закончена");
        
    }
}