using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public int id;
    public double damage;
    public bool isUse;
    public float timeDamage;
    private float timeLeft = 0;

    void Update()
    {
        if(timeLeft >= 0) timeLeft -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.tag == "Enemy"){
            if (timeLeft < 0) {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.enemyHealth = enemy.enemyHealth - damage;
                Debug.Log(enemy.enemyHealth);
                enemy.Death();
                timeLeft = timeDamage;
            }
        }
    }
}
