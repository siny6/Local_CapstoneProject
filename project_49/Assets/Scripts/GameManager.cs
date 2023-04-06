using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnManager pool;
    public GameObject player;
    public GameObject coor;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
