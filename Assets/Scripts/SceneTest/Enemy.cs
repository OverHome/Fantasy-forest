using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public double enemyHealth;
    public float timeDamage;
    private float timeLeft = 0;
    private double damageEnemy = 10;

    void Update()
    {
        if(timeLeft >= 0) timeLeft -= Time.deltaTime;
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
    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.tag == "Player"){
            if (timeLeft < 0) {
                Player player = collision.gameObject.GetComponent<Player>();
                player.health = player.health - damageEnemy;
                Debug.Log(player.health);
                player.Death();
                timeLeft = timeDamage;
            }
        }
    }
}
