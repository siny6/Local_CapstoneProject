using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_002_Normal_Ranger : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;
    public GameObject prefabBullet;
    

    public override void InitEnemyStatusCustom()
    {
        hpFull = 15;
        damage = 3f;
        hp = 15;
        speed = 2;
        attackSpeed = 1f;

        firePoint = transform.GetChild(0);
        target = GameManager.gm.player.gameObject;
        prefabBullet =  Resources.Load<GameObject>("Prefabs/Enemies/Enemy_Bullet");
    }
    protected override void AttackCustom()
    {
        GameObject proj = Instantiate(prefabBullet, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile_Enemy>().SetUp(damage, 5, 1, 0, 0, 5f);
        proj.GetComponent<Projectile_Enemy>().SetDirection(target.transform);
        proj.GetComponent<Projectile_Enemy>().RotateProj();
        proj.GetComponent<Projectile_Enemy>().Action();
    }

    public override void DieCustom()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        hp = hpFull;
    }

    public override void MoveCustom()
    {

        distance = Vector3.Distance(transform.position, base.target.transform.position);
        dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치

        if (distance >= 10)
        {   
            rb.MovePosition(transform.position + nextVec);
        }
        else
        {
            rb.MovePosition(transform.position - nextVec);
        }
        

    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "002";
    }
}
