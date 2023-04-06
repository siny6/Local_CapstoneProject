using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] makePoints; // 몬스터 생성 위치를 담는 배열
    float timer;
    float spawnRate = 2f; // 생성주기
    Vector2 spawnPos;
    public GameObject Coor;
    

    void Awake()
    {
        //makePoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        spawnPos = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));

        if (timer > spawnRate)
        {
            timer = 0;
            GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 4));
            //enemy.transform.position = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_trailer(spawnPos));
            enemy.transform.position = spawnPos;

        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));
            //enemy.transform.position = makePoints[Random.Range(1, makePoints.Length)].position;
            enemy.transform.position = spawnPos;
        }
    }
    IEnumerator create_trailer(Vector2 pos)
    {
        GameObject coor = Instantiate(Coor, transform.position, Quaternion.identity);
        coor.transform.position = pos;
        //for (int i = 0; i < 3; i++)
        //{
        //    coor.color = new Color(1, 0, 0);
        //    yield return new WaitForSeconds(0.5f);
        //    rend.color = new Color(0, 1, 0);
        //    yield return new WaitForSeconds(0.5f);
        //}
        yield return new WaitForSeconds(1f);
        Destroy(coor);


    }
}
