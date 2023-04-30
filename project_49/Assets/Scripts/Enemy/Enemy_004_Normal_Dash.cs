using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_004_Normal_Dash : Enemy
{
    public float dashSpeed = 5;
    public float defaultSpeed = 2;

    public float dashDamage = 2;
    public float defaultDamage = 1;

    bool isDash = false;

    public GameObject particle_charging;
    public override void InitEnemyStatusCustom()
    {
        hpFull = 20;
        hp = 20;
        speed = defaultSpeed;
        attackSpeed = 0.1f;
        damage = defaultDamage;
    }

    public override void MoveCustom()
    {
        if (isDash)
        {
            StartCoroutine(Dash());
        }
        else
        {
            Vector3 dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
            Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
            rb.MovePosition(transform.position + nextVec);
            rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정
        }
        
    }
    protected override void AttackCustom()
    {
        //StartCoroutine(Dash());

    }

    public override void DieCustom()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        hp = hpFull;
    }

    IEnumerator Dash()
    {
        //speed = 0;
        // 돌진 전 전조
        // youtube magic charge particle - particle system unity tutorial
        GameObject charge = Instantiate(particle_charging, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Destroy(charge);

        //SpriteRenderer rend = GetComponent<SpriteRenderer>();
        //for (int i = 1; i <= 3; i++)
        //{
        //    rend.color = new Color(200+i, 0, 0);
        //    yield return new WaitForSeconds(0.5f);
        //}

        speed = dashSpeed; // 속도 증가
        damage = dashDamage; // 데미지 증가
        yield return new WaitForSeconds(1.5f);
        //rend.color = new Color(1, 0, 1);
        speed = defaultSpeed;
        damage = defaultDamage;
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "004";
    }
}
