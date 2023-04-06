using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 0;

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if(lifeTime > 5f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //player�� ü�°��� �Լ�
                Destroy(gameObject);
            }
        }

    }


}
