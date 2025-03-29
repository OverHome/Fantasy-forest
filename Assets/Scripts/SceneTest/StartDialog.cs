using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : MonoBehaviour
{   
    public GameObject background;
    public GameObject NPC;
    private GameObject test;
    public Camera mainCam;
    private bool flasher = false;

    private float interactionDistance = 3f;

    void Start()
    {
        background.SetActive(flasher);
    }

    void Update()
    { 
        Interaction();
    }
    void Interaction()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            test = hit.collider.gameObject;
            if(test == NPC)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    flasher = !flasher;
                    background.SetActive(flasher);
                }
            }
        }
    }
}
