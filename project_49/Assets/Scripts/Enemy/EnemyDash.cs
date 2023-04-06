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
        Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // ������ �ӵ� 0���� ����
    }

    protected override void AttackCustom()
    {
        StartCoroutine(Dash());
        
    }

    IEnumerator Dash()
    {
        speed = dashSpeed; // ��������
        yield return new WaitForSeconds(1f);
        speed = oriSpeed;
    }


}
