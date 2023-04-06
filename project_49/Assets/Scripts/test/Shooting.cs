using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bp;
    public GameObject target;

    public float bulletSpeed = 10f;

    float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            GameObject bullet = Instantiate(bp, firepoint.position, firepoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            if (target.transform.position.x < firepoint.position.x)
            {
                rb.AddForce(firepoint.right * bulletSpeed, ForceMode2D.Impulse);
            }
            else
            {
                //rotation
                rb.AddForce(firepoint.right * bulletSpeed, ForceMode2D.Impulse);
            }
            

            
            
            Destroy(bullet, 5f);
            timer = 0f;
        }
        
    }
    void OnEnable()
    {
        target = GameManager.instance.player; // 따라갈 대상 = 플레이어 // 따라갈 대상 = 플레이어
    }
}
