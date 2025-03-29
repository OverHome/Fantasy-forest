using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayImitation : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Button Startbutton;
    public Button Option1;
    public Button Option2;
    public Button End;
    public Button Next;
    private float timeLeft = 0;
    private float cooldown = 1;
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if ( timeLeft < 0 )
        {
            //Check if the left Mouse button is clicked
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;
                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();
                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.name == Startbutton.name)
                    {
                        result.gameObject.GetComponent<Button>().onClick.Invoke();
                        Debug.Log("Hit " + result.gameObject.name);
                        timeLeft = cooldown;

                        break;
                    }
                    else if(result.gameObject.name == Option1.name)
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
    }
}
