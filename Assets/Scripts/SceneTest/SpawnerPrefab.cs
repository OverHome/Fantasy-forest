using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPrefab : MonoBehaviour
{
    public GameObject enemy;
    public double cooldown;
    private double timeLeft;
    private int slimeperspawner = 6;

    void Update()
    {
        if(timeLeft > 0) timeLeft -= Time.deltaTime;
        if(timeLeft <= 0 && GameObject.FindGameObjectsWithTag("Enemy").Length < 4*slimeperspawner) Invoke("Spawner", 0f);
    }
    void Spawner()
    {
        GameObject instance = Instantiate(enemy, this.transform);
        timeLeft = cooldown;
    }
}
