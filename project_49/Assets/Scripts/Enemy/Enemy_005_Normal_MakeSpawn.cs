using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_005_Normal_MakeSpawn : Enemy
{
    // 행동특징
    // 1. 플레이어에게서 도망간다.
    // 2. 일정시간마다 기본 몬스터 3마리를 소환한다.

    // 버그
    // 플레이어 한테 멀어지려고 할 때 스테이지 범위(collider)를 뚫는다.
    // body type : kinematic --> dynamic 으로 바꾸니까 계속 스테이지 끝에 있으면 뚫리긴 하는데 잘 안뚫음.
    // idea : collision 감지할 때 스테이지 끝에 닿으면 뒤로 물러나기 코드제어

    public float distance;
    Vector3 dirVec;

    float timer;
    float spawnRate = 7f;
    Vector2 spawnPos;

    public override void DieCustom()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        hp = hpFull;
    }

    public override void InitEnemyStatusCustom()
    {
        hpFull = 20;
        hp = 20;
        damage = 1;
        attackSpeed = 7f;
        speed = 3f;

    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "005";
    }

    public override void MoveCustom()
    {
        distance = Vector3.Distance(transform.position, base.target.transform.position);
        dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치

        if (distance >= 10)
        {
            rb.MovePosition(transform.position + nextVec);

        }
        else
        {
            rb.MovePosition(transform.position - nextVec);
        }
    }

    protected override void AttackCustom()
    {
        // 플레이어가 닿으면 플레이어의 체력이 감소
    }

    private void Update()
    {
        // spawnPos = transform.position;
        timer += Time.deltaTime;
        if (timer > spawnRate)
        {
            timer = 0;
            for(int i=0; i< Random.Range(1, 3); i++)
            {
                GameObject enemy = GameManager.gm.pool.Get(0);
                enemy.transform.position = transform.position;
            }
            
        }
    }



}
