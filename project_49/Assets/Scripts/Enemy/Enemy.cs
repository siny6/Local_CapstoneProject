using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 부모 클래스
public abstract class Enemy : MonoBehaviour
{
    protected float speed; // 이동속도
    protected float hp; // 체력
    protected float damage; // 공격력
    
    public float attackSpeed;   // 공격속도(초당 공격속도)
    
    protected bool state = true; // 현재 상태 true : 기본상태, false : 상태이상
    public float lastAttackTime;

    public bool canAttack       // 공격 가능 상태
    {
        get
        {
            // 공격 간격 과 상태이상(cc기 ) 판별
            float attackDelay = 1 / attackSpeed;
            if (lastAttackTime + attackDelay <= Time.time)
            {
                return true;
            }
            return false;
        }
    }


    public GameObject[] item; // 체력이 없을 때 드랍하는 아이템 리스트

    protected Rigidbody2D rigid;

    public GameObject target; // 따라갈 대상

    //=====================================================================================

    // 초기화?
    public abstract void InitEnemyStatus();

    // 공격
    protected abstract void AttackCustom();


    // player의 공격에 충돌 Player.cs에서 처리
    protected void OnTriggerEnter2D(Collider2D other)
    {
  

    }

    // 아이템(exp) 드랍
    public void DropItem()
    {
        for (int i = 0; i < item.Length; i++)
            Instantiate(item[i], transform.position, Quaternion.identity);
    }

    // 죽음
    protected void Death(GameObject obj)
    {
        obj.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = true;
        hp = 100;
        DropItem();
    }

    // 공격 받음
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
    // 무기의 전투 플로우
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
    // 공격_공통
    //=======================================
    public void Attack()
    {
        // 공격가능한 상황일때 공격
        if (canAttack)
        {
            lastAttackTime = Time.time;   // 마지막 공격 시간 갱신

            AttackCustom();
        }
    }



    // 생성되면, 
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player; // 따라갈 대상 = 플레이어

        // 초기화 하고 전투 플로우 시작 ( 타겟 찾고, 공격 )
        InitEnemyStatus();
        StartCoroutine(BattleFlow());
    }

    void Update()
    {
        MoveCustom();
    }


    public abstract void MoveCustom();
}
