using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public double health;
    
    public void Death()
    {
        if(health <= 0)
        {
            Debug.Log("Death");
        }
    }
    void Regeneration()
    {

    }
}
