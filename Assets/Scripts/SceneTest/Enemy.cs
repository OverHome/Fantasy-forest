using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public double enemyHealth;
    
    void Update()
    {
        
    }
    public void Death()
    {
        if(enemyHealth<=0)
        {
            Destroy(gameObject);
        }
    }
    void regenHealth()
    {
        
    }
}
