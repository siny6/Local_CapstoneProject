using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickbutton : MonoBehaviour
{
    public GameObject enemy;

    public void OnClick()
    {
        Instantiate(enemy);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
