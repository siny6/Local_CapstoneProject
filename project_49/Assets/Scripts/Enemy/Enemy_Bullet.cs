using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : Projectile_Enemy
{
    public void SetUp()
    {
        damage = 5;
    }
    public override void Action()
    {
        base.rb.velocity = transform.up * base.speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float dmg = damage;

            if (dmg != 0)
            {
                GameManager.gm.player.OnDamage(dmg);
            }

            //rigid.isKinematic = true;
        }
    }
}
