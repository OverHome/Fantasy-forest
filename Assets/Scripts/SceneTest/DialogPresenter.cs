using System; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

///Набор все возможных состояний диалога
enum DialoguePresenterState
{
    Await, ///< Указывает на ожидание диалога
    Talk, ///< Указывает что диалог идет
    Choice, ///< Указывает на выбор в диалоге
    Finish ///< Указывает на завершение ветки диалога
}
//Класс, описывающий взаимодействие с диалоговым NPC
public class DialogPresenter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private DialogView _dialogueView;
    [SerializeField] private TextAsset[] _dialoguesArr;
    private uint _currentDialogue;
    private DialogNode _currentNode;
    public Button Next; ///< Кнопка для продолжение диалога

    [Header("Events")]
    [SerializeField] private UnityEvent[] OnDialogueFinished;
    public event Action OnDialogueStart; ///< Ивент старта диалога

    [field: Header("States")]
    [field: SerializeField] public bool CanTalk { get; set; } ///< Указывает что NPC прямо сейчас говорит
    [SerializeField] private DialoguePresenterState _state;

    [field: Header("Interact")]
    [SerializeField] public GameObject background; ///< Диалоговое окно
    [SerializeField] public GameObject NPC; ///< NPC с кем происходит диалог
    [SerializeField] public Camera mainCam; ///< Камера персонажа
    private GameObject test;
    private bool flasher = false;
    private float interactionDistance = 3f;
    
    void Update()
    { 
        Interaction();
    }
    /*!
    Пускается рейкаст, если рейкаст поподает на NPC и
    находится на определенном расстоянии, то всплывает диалоговое окно
    */
    public void Interaction()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            test = hit.collider.gameObject;
            if(test == NPC)
            {
                if(Input.GetKeyDown(KeyCode.F) && !flasher)
                {
                    flasher = !flasher;
                    background.SetActive(flasher);
                    StartDialogue();
                }
            }
        }
    }

    public void Inter()
    {
        flasher = !flasher;
        background.SetActive(flasher);
        StartDialogue();
    }
    /*!
    Действия при старте диалога
    */
    public void StartDialogue()
    {
        if (_state == DialoguePresenterState.Finish || !CanTalk)
            return;

        _currentNode = ParseDialogFile.GetDialogTree(_dialoguesArr[_currentDialogue]);
        _dialogueView.SetPresenter(this);
        OnDialogueStart?.Invoke();
        Next.gameObject.SetActive(true);
        CanTalk = false;
        _dialogueView.StartDialogue(_currentNode.Message, _currentNode.Name);
        _state = DialoguePresenterState.Talk;
    }
    /*!
    Действия при продолжения диалога
    */
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
    /*!
    Действия при завершения диалога
    */
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
    /*!
    Смена диалоговой ветки
    */
    public void SwitchBranch(int index)
    {
        Debug.Log("Button clicked " + index.ToString());
        _currentNode = _currentNode.Children[index];
        _state = DialoguePresenterState.Talk;
        _dialogueView.NextMessage(_currentNode.Message, _currentNode.Name);
    }
    /*!
    Действие при выборе первого варианта
    */
    public void Action0()
    {
        Debug.Log("Первая ветка закончена");
        flasher = !flasher;
    }
    /*!
    Действие при выборе второго варианта
    */
    public void Action1()
    {
        Debug.Log("Вторая ветка закончена");
        
    }
}