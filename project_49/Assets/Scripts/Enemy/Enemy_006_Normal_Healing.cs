using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_006_Normal_Healing : Enemy
{
    public GameObject heal_zone; // 파티클이나 마법진 등 보여주기
    public float radius = 5; // heal_zone 반지름
    public float heal_hp = 3; // 얼마나 회복
    public float distance;
    Vector3 dirVec;

    public override void InitEnemyStatusCustom()
    {
        hpFull = 20;
        hp = 20;
        damage = 1;
        attackSpeed = 7f;
        speed = 3f;
    }

    public override void MoveCustom()
    {
        // 개선사항 : 플레이어를 따라가지말고 주변 제일 가까운 일반몬스터 or 많이 모여있는 일반 몬스터 따라가서 머무르기

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

    protected override void AttackCustom()
    {
        StartCoroutine(HealEnemy());
    }

    public override void DieCustom()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        hp = hpFull;
    }

    IEnumerator HealEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        var hitCollider = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in hitCollider)
        {
            var ply = hit.GetComponent<Enemy>();
            if (ply)
            {
                var closePoint = hit.ClosestPoint(transform.position);
                var dis = Vector3.Distance(closePoint, transform.position);

                var healingPercent = Mathf.InverseLerp(radius, 0, dis);
                //var damagePercent = bombDamage - (dis / 10);
                ply.Healing(healingPercent * heal_hp);
            }
        }
        //GameObject explosion = Instantiate(particle_Boom, transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(1f);
        //Destroy(explosion);
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "006";
    }
}
