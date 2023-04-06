using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : Enemy
{
    float dashSpeed = 5;
    float oriSpeed = 2;
    public override void InitEnemyStatus()
    {
        hp = 20;
        speed = 2;
        attackSpeed = 0.01f;
    }

    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리적 속도 0으로 고정
    }

    protected override void AttackCustom()
    {
        StartCoroutine(Dash());
        
    }

    IEnumerator Dash()
    {
        speed = dashSpeed; // 순간가속
        yield return new WaitForSeconds(1f);
        speed = oriSpeed;
    }


}
