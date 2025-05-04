using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*! Класс отвечающий за нажатие кнопок на World Canvas*/
public class PlayImitation : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Button Option1; ///< Кнопка выбора 1
    public Button Option2; ///< Кнопка выбора 2
    public Button End; ///< Кнопка конца диалога
    public Button Next; ///< Кнопка продолжение диалога
    private float timeLeft = 0;
    private float cooldown = 1;

    /*! Инициализирует переменные графического рекастера и ивентовой системы */
    public void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }
    /*! Пускает рейкаст каждые n тиков, если рейкаст поподает по объекту Button
    инициирует вызов метода вызывающие функцию кнопки */
    public void Update()
    {
        if ( timeLeft < 0 )
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_PointerEventData = new PointerEventData(m_EventSystem);
                
                m_PointerEventData.position = Input.mousePosition;
                
                List<RaycastResult> results = new List<RaycastResult>();
                
                m_Raycaster.Raycast(m_PointerEventData, results);
                
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.name == Option1.name)
                    {
                        result.gameObject.GetComponent<Button>().onClick.Invoke();
                        Debug.Log("Hit " + result.gameObject.name);
                        timeLeft = cooldown;
                        break;
                    }
                    else if(result.gameObject.name == Option2.name)
                    {
                        result.gameObject.GetComponent<Button>().onClick.Invoke();
                        Debug.Log("Hit " + result.gameObject.name);
                        timeLeft = cooldown;
                        break;
                    }
                    else if(result.gameObject.name == End.name)
                    {
                        result.gameObject.GetComponent<Button>().onClick.Invoke();
                        Debug.Log("Hit " + result.gameObject.name);
                        timeLeft = cooldown;
                        break;
                    }
                    else if(result.gameObject.name == Next.name)
                    {
                       result.gameObject.GetComponent<Button>().onClick.Invoke();
                       Debug.Log("Hit " + result.gameObject.name);
                       timeLeft = cooldown;
                       break;
                    }
                }
            }
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }
}
