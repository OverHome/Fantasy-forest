using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public int id;
    public double damage;
    public double timeDamage;
    public bool isUse;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.enemyHealth = enemy.enemyHealth - damage;
            Debug.Log(enemy.enemyHealth);
            enemy.Death();
        }
    }
}
