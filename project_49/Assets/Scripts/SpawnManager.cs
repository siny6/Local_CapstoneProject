using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs; // 프리펩들(몬스터)을 저장할 배열 변수 선언, 다양한 모양의 몬스터 저장가능
    List<GameObject>[] pools; // 프리펩들을 담는 배열 선언

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i=0; i<pools.Length; i++)
        {
            // pools 리스트 초기화
            pools[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int i)
    {
        GameObject select = null;
        foreach(GameObject item in pools[i])
        { // 배열 순회
            if (!item.activeSelf)
            { // 현재 안쓰고 있는 오브젝트(몬스터)가 있나?
                select = item;
                select.SetActive(true); // 안쓰고 있는 거 활성화
                break;
            }
        }

        if (!select)
        { // 안쓰는 오브젝트를 찾지 못하면 생성
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }


        return select;
    }
}
