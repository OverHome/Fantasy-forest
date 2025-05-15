using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public double health;
    public Transform SpawnPoint;

    public void Death()
    {
        if(health <= 0)
        {
            //Debug.Log("Death");
            this.transform.position = new Vector3 (SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z);
            health = 100;
        }
    }
    void Regeneration()
    {

    }
}
