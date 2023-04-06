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

        dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리적 속도 0으로 고정
        //movement = new Vector2(moveX, moveY).normalized;

    }

    private void FixedUpdate()
    {
        //rigid.velocity = new Vector2(movement.x * speed, movement.y * speed);
        float aimAngle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg - 90f;
        rigid.rotation = aimAngle;
        
    }
}
