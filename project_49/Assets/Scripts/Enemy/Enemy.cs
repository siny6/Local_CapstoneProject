using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �θ� Ŭ����
public abstract class Enemy : MonoBehaviour
{
    protected float speed; // �̵��ӵ�
    protected float hp; // ü��
    protected float damage; // ���ݷ�
    
    public float attackSpeed;   // ���ݼӵ�(�ʴ� ���ݼӵ�)
    
    protected bool state = true; // ���� ���� true : �⺻����, false : �����̻�
    public float lastAttackTime;

    public bool canAttack       // ���� ���� ����
    {
        get
        {
            // ���� ���� �� �����̻�(cc�� ) �Ǻ�
            float attackDelay = 1 / attackSpeed;
            if (lastAttackTime + attackDelay <= Time.time)
            {
                return true;
            }
            return false;
        }
    }


    public GameObject[] item; // ü���� ���� �� ����ϴ� ������ ����Ʈ

    protected Rigidbody2D rigid;

    public GameObject target; // ���� ���

    //=====================================================================================

    // �ʱ�ȭ?
    public abstract void InitEnemyStatus();

    // ����
    protected abstract void AttackCustom();


    // player�� ���ݿ� �浹 Player.cs���� ó��
    protected void OnTriggerEnter2D(Collider2D other)
    {
  

    }

    // ������(exp) ���
    public void DropItem()
    {
        for (int i = 0; i < item.Length; i++)
            Instantiate(item[i], transform.position, Quaternion.identity);
    }

    // ����
    protected void Death(GameObject obj)
    {
        obj.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = true;
        hp = 100;
        DropItem();
    }

    // ���� ����
    protected void Damaged(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Death(gameObject);
        }
    }

    //=========================================================================================
    //===================================
    // ������ ���� �÷ο�
    //===================================
    public IEnumerator BattleFlow()
    {
        while (true)
        {
            AttackCustom();

            float attackDelay = 1 / attackSpeed;
            yield return new WaitForSeconds(attackDelay);
        }
    }

    //=======================================
    // ����_����
    //=======================================
    public void Attack()
    {
        // ���ݰ����� ��Ȳ�϶� ����
        if (canAttack)
        {
            lastAttackTime = Time.time;   // ������ ���� �ð� ����

            AttackCustom();
        }
    }



    // �����Ǹ�, 
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player; // ���� ��� = �÷��̾�

        // �ʱ�ȭ �ϰ� ���� �÷ο� ���� ( Ÿ�� ã��, ���� )
        InitEnemyStatus();
        StartCoroutine(BattleFlow());
    }

    void Update()
    {
        MoveCustom();
    }


    public abstract void MoveCustom();
}
