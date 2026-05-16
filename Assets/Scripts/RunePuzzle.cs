using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePuzzle : MonoBehaviour
{
    public List<GameObject> runes;
    public GameObject sword;
    public void Check()
    {
        print(runes.Count);
        if (runes.Count != 4) return;
        bool isGood = true;
        for (int i = 0; i < runes.Count; i++)
        {
            if (runes[i].GetComponent<Runes>().id != i)
            {
                isGood = false;
                break;
            }
        }

        if (isGood)
        {
            sword.SetActive(true);
        }
        else
        {
            for (int i = 0; i < runes.Count; i++)
            {
                runes[i].GetComponent<Runes>().Down();
            }
            runes.Clear();
        }
    }
}
