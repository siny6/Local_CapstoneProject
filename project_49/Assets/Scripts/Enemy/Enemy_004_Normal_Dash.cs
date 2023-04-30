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
            Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
            Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
            rb.MovePosition(transform.position + nextVec);
            rb.velocity = Vector2.zero; // ������ �ӵ� 0���� ����
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
        // ���� �� ����
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

        speed = dashSpeed; // �ӵ� ����
        damage = dashDamage; // ������ ����
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
