using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoolTest : MonoBehaviour
{
    int enemyCnt = 0;
    Vector2 spawnPos;
    float spawnLate = 3f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnPos = new Vector2(Random.Range(0, 20f), Random.Range(0, 20f));
        
    }
}
