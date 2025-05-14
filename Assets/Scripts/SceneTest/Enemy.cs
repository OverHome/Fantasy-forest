using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public double enemyHealth;
    public float timeDamage;
    private float timeLeft = 0;
    public double damageEnemy = 10;

    private bool backstepswitch = true;
    private Vector3 backstep;
    public float speed = 1;
    public GameObject player;
    public LayerMask whatIsPlayer;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        backstep = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
        if(timeLeft >= 0) timeLeft -= Time.deltaTime;
        Attack();
    }
    public void Death(GameObject obj)
    {
        if(enemyHealth<=0)
        {
            Destroy(obj);
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
                player.Death();
                timeLeft = timeDamage;
            }
            this.transform.position = backstep;
            backstepswitch = !backstepswitch;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    void Attack()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(playerInSightRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Vector3 targetPostition = new Vector3(player.transform.position.x, 
                                       this.transform.position.y, 
                                       player.transform.position.z );
            this.transform.LookAt(targetPostition);     
        }
        if(playerInAttackRange && backstepswitch)
        {
            backstep = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            backstepswitch = !backstepswitch;
        }
    }
}
