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
        Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // ������ �ӵ� 0���� ����
    }


}
