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
    // �÷��̾� ���� ���ϴ� �������� ���̰� �־�� ��.
    // �÷��̾����� ��Ƽ� ENEMY�� ü���� �ٴ� ���� �ƴ϶� �÷��̾��� ���ݿ� �޾�����

    private void FixedUpdate()
    {
        
        distance = Vector2.Distance(transform.position, base.target.transform.position);
        
        if(distance < 3f)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            // �÷��̾� ���ݿ� �浹���� ���� ����.
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
        Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero; // ������ �ӵ� 0���� ����
    }
}
