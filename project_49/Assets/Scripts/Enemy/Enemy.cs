using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float speed; // �̵��ӵ�
    
    [SerializeField] // ************************************************************** �̰��ϸ� private�̳� protected�� �ν����� â���� �� �� ����
    public float hp; // ü��

    public float hpFull;     // ****************

    protected float damage; // ���ݷ�

    public float attackSpeed;   // ���ݼӵ�(�ʴ� ���ݼӵ�)

    protected bool state = true; // ���� ���� true : �⺻����, false : �����̻�
    public float lastAttackTime;
    public bool canAttack       // ���� ���� ����
    {
        get
        {
            if (attackSpeed == 0)                
            {
                return false;
            }            
            // ���� ���� �� �����̻�(cc�� ) �Ǻ�
            float attackDelay = 1 / attackSpeed;
            if (lastAttackTime + attackDelay <= GameManager.gm.gameTime)
            {
                return true;
            }
            return false;
        }
    }
    public GameObject[] item; // ü���� ���� �� ����ϴ� ������ ����Ʈ

    protected Rigidbody2D rb;

    public GameObject target; // ���� ���

    public string id_enemy;

    //==========
    //애니메이션 관련                   //  *******************************************************************************
    protected Animator animator;

    [SerializeField]
    public bool isDead = false;

    [SerializeField]
    public Vector3 originScale;



    //=====================================================================================

    // 초기화작업 (공통)                         
    public void InitEnemyStatus()
    {
        InitEnemyStatusCustom();                    // 개별 능력치 먼저 초기화
        
        GetComponent<Collider2D>().enabled = true;
        transform.localScale = originScale;

        isDead = false;
        hp = hpFull;
    }

    // 개별 능력치 초기화
    public abstract void InitEnemyStatusCustom();

    public abstract void InitEssentialEnemyInfo();

    // ������(exp) ���
    public void DropItem()
    {
        // for (int i = 0; i < item.Length; i++)
        //     Instantiate(item[i], transform.position, Quaternion.identity);
        // Instantiate(item[0], transform.position, Quaternion.identity);   
        
        //�ϴ��� ����ġ ����� - ���߿� Ȯ�������� �ڼ�, ȸ�������� ����� ���� ********************************
        GameObject dropItem = Instantiate(item[0], GameObject.Find("Items").transform);

        dropItem.transform.position = transform.position;
    }

    // ����
    protected void Death()                                  //********************************
    {
        //GetComponent<Collider2D>().enabled = false;
        

        float animationLength = 0;
        if (animator !=null)
        {
            animator.SetTrigger("isDead");   // 죽는 애니메이션 재생   *******************************************************************************
            animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;  // 애니메이션 길이 측정
        }
       
        
        GameManager.gm.KillCount += 1;

        DieCustom();
        
        DropItem();

        Invoke("GoBackToPool", animationLength);    // 애니메이션 끝나고 풀로 보내버리기 

    }

    // 해당 적을 비활성화하여 풀로 보내버리기
    public void GoBackToPool()          //********************************
    {
        EnemyPoolManager.epm.TakeToPool(this);
  



        // InitEnemyStatus();      // 능력치, 정보 초기화 
    }


    // public void Disappear()       //********************************
    // {
    //     gameObject.SetActive(false);
    //     GetComponent<Collider2D>().enabled = true;
    //     hp = hpFull;
    // }

    // ���� ����
    public void Damaged(float damage)
    {
        hp -= damage;

        int prob = Random.Range(1, 101);
        if (prob <= target.GetComponent<Player>().Drain_prob)
            target.GetComponent<Player>().ChangeHp(target.GetComponent<Player>().Drain);

        if (hp <= 0)
        {
            Death();
        }
    }

    public void Healing(float heal)
    {
        // 1. 마지막 공격받은 시간에서 일정시간 지나면 스스로 힐
        // 2. 버프몬스터 근처에 있다가 힐 받음
        hp += heal;
    }

    //=========================================================================================
    //===================================
    // ������ ���� �÷ο�
    //===================================
    public IEnumerator BattleFlow()
    {
        while (!isDead)     // 죽은 상태가 아닐때 전투플로우 
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
            lastAttackTime = GameManager.gm.gameTime;   // ������ ���� �ð� ����

            AttackCustom();
        }
    }

    // ����
    protected abstract void AttackCustom();

    //==============================================
    void Awake()
    {
        originScale = transform.localScale;         // 가장 처음
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();  //  *******************************************************************************

        target = GameManager.gm.player.gameObject; // ���� ��� = �÷��̾�

        // �ʱ�ȭ �ϰ� ���� �÷ο� ����
        // InitEnemyStatus();
        
    }

    // 풀에서 나왔을때 ( 비활성 -> 활성) 전투 루틴 시작
    void OnEnable()
    {
        gameObject.AddComponent<EnemyType>();       // 에너미 타입 결정 : 일단은 분열 작동시키기 위해.
        GetComponent<EnemyType>().InitType();

        InitEnemyStatus();  
        StartCoroutine(BattleFlow());
    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            MoveCustom();
        }
    }


    public abstract void MoveCustom();

    public abstract void DieCustom(); //********************************




    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            float dmg = damage;

            if (dmg != 0 )
            { 
                GameManager.gm.player.OnDamage(dmg);
            }

            //rigid.isKinematic = true;
        }
    }

    //// 넉백
    ///넉백 힘 무기별 차이점
    //1. 넉백 지속시간 동안 이동불가
    //2. velocity = 
    // 3. or adforce impulse rigidbody 모드

}
