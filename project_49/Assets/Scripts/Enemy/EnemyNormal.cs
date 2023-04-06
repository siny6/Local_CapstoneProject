using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : Enemy
{
    protected override void AttackCustom()
    {
        
    }

    public override void InitEnemyStatus()
    {
        base.hp = 100;
        base.speed = 2;
        base.attackSpeed = 0.0000000001f;
    }

    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리적 속도 0으로 고정
    }


}
