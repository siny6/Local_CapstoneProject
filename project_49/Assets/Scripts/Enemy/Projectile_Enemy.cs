using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=======================부모클래스===================================
// 공격시 발생하며, 적에게 피해를 입힐 수 있는 투사체나 공격효과 등에 관한 클래스 - 자식 클래스의 이름은 "Proj_(이름)"으로 작명 
//  ex )  총알, 광선, 검흔 등 
//===================================================================
public abstract class Projectile_Enemy : MonoBehaviour
{
    // 필요 변수
    protected Rigidbody2D rb;      // 물리 조정을 위한 리지드바디
    
    // 필수 능력치
    public float damage;               // 공격력 ( 무기에서 받아오자 )
    public float speed;                // 탄속
    public float scale;                 // 크기 (공격범위)
    public int projNum;                  //투사체수
    public int penetration;            // 관통 횟수    (-1이면 무한)
    
    public float lifeTime;               // 탄환 지속시간
    
    // 선택 능력치
    public Transform target;             // 적의 위치 
    public Vector3 direction;            // 발사방향 

    //분열 관련 
    public GameObject prefab_parent;
    public GameObject prefab_proj;

    
    //=============================================
    // projectile의 셋업
    // 데미지, 탄속, 크기, 관통, 분열횟수, 수명
    // =============================================
    public void SetUp(float dmg, float spd, float sc, int pn, int sn, float lt)
    {
        rb = GetComponent<Rigidbody2D>();
        
        damage = dmg;
        speed = spd;
        scale = sc;
        projNum = pn;
        //penetration = pen;
        
        lifeTime = lt;

        // lifeTime 이 -1인 경우는 무기가 영구지속
        if (lifeTime != -1)
        {
            Destroy(gameObject, lifeTime);
        }

        transform.localScale =  transform.localScale * scale;


    }

    //============================================
    // 타겟 세팅 - 
    //============================================
    public void SetTarget(Transform target)
    {
        if (target == null)
        {
            return;
        }

        this.target = target;
    }
    
    //============================================
    // 발사 방향 세팅 - 2종류 있음 : 타겟을 주고 방향을 계산, 방향을 직접 주기
    //============================================
    public void SetDirection(Transform target)      
    {
        if (target == null)
        {
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        this.direction = dir;
    }

    public void SetDirection(Vector3 dir)
    {
        this.direction =dir;
    }

    //============================================
    // 투사체 회전시키기    : 현재 방향에서 주어진 각도만큼회전
    //============================================
    public void RotateProj(float angle) // 회전각이 직접 주어짐 
    {
        transform.Rotate(new Vector3(0,0,1) * angle);
    }

    public void RotateProj()    // 방향이 주어짐 
    {
        if (direction == null)
        {
            return;
        }
        transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
    }




    //============================================
    // projectile 액션 (모션, 애니메이션)
    //============================================
    public abstract void Action();

    //==============================================
    // 충돌시 - 적 충돌을 감지 
    //==============================================
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            if (other.GetComponent<Player>() != null)
            {
                GameManager.gm.player.OnDamage(damage);
            }

            Destroy(gameObject);
        }
    }




    //==============================================
    // 관통처리 
    //==============================================
    //void Penetrate()
    //{
    //    if (penetration == -99)
    //    {
    //        return;
    //    }

        
    //    // 관통횟수 감소 후 삭제
    //    if (penetration--<=0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}



}
