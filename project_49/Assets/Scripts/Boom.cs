using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float range = 1;
    public float bombDamage = 2;

    private void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (range > 0)
        {
            var hitCollider = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (var hit in hitCollider)
            {
                var ply = hit.GetComponent<Player>();
                if (ply)
                {
                    var closePoint = hit.ClosestPoint(transform.position);
                    var dis = Vector3.Distance(closePoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(range, 0, dis);
                    ply.Damaged(damagePercent * bombDamage);
                }
            }
        }

    }

}
