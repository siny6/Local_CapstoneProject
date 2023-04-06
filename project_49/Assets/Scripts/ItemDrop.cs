using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    Rigidbody2D itemRb;
    
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        //itemRb.velocity = Vector2.zero; // 물리적 속도 0으로 고정
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {

                Destroy(gameObject,0.1f); // 자신의 게임 오브젝트 파괴
            }
        }

    }
}
