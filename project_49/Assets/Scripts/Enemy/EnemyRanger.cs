using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : Enemy
{
    public override void InitEnemyStatus()
    {
        hp = 20;
        speed = 2;
        attackSpeed = 3f;
    }
    protected override void AttackCustom()
    {
        
    }

    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;
    public BulletSpawner bulletspawner;

    public override void MoveCustom()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // ������ �ӵ� 0���� ����
        //movement = new Vector2(moveX, moveY).normalized;

    }

    private void FixedUpdate()
    {
        //rigid.velocity = new Vector2(movement.x * speed, movement.y * speed);
        float aimAngle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg - 90f;
        rigid.rotation = aimAngle;
        
    }
}
