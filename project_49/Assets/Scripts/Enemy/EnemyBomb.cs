using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : Enemy
{
    float distance;
    //public float damage = 10;
    public float range;

    public override void InitEnemyStatus()
    {
        speed = 2;
        attackSpeed = 0.1f;
        hp = 20;
    }

    protected override void AttackCustom()
    {

    }
    // 플레이어 한테 가하는 데미지는 차이가 있어야 함.
    // 플레이어한테 닿아서 ENEMY의 체력이 다는 것이 아니라 플레이어의 공격에 받았을대

    private void FixedUpdate()
    {
        
        distance = Vector2.Distance(transform.position, base.target.transform.position);
        
        if(distance < 3f)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            // 플레이어 공격에 충돌하지 않음 무적.
            StartCoroutine(Bomb());
        }
    }

    IEnumerator Bomb()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        Collider2D coll = GetComponent<Collider2D>();


        for (int i=0; i<3; i++)
        {
            rend.color = new Color(1, 0, 0);
            yield return new WaitForSeconds(0.5f);
            rend.color = new Color(0, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }     
        
        Death(gameObject);
    }


    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리적 속도 0으로 고정
    }
}
