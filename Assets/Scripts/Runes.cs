using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runes : MonoBehaviour
{
    public int id;
     GameObject Lite;
     AudioSource audio;
    public RunePuzzle Puzzle;

    private void Start()
    {
        Lite = transform.GetChild(0).gameObject;
        audio = GetComponent<AudioSource>();
    }

    public void Inter()
    {
        audio.Play();
        Lite.SetActive(true);
        Puzzle.runes.Add(gameObject);
        Puzzle.Check();
    }
    
    public void Down()
    {
        Lite.SetActive(false);
    }
}
