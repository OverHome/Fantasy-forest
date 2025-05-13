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

    void OnTriggerEnter(Collider collision)
    {   
        if(collision.gameObject.tag == "Enemy"){
            if (timeLeft < 0) {
                GameObject enemyу = collision.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                Enemy enemy = enemyу.GetComponent<Enemy>();
                enemy.enemyHealth = enemy.enemyHealth - damage;
                Debug.Log(enemy.enemyHealth);
                enemy.Death(enemyу);
                timeLeft = timeDamage;
            }
        }
    }
}
