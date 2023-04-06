using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDelete : MonoBehaviour
{
    public GameObject enemy;
    public void OnClick()
    {
        Destroy(enemy);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
