using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    [Header("速度")]
    public float speed = 5f;
    [Header("傷害")]
    public int damage = 35;


    public float enemyhp = 10;

    private Rigidbody2D r2d;
    public Transform checkPoint;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, -checkPoint.up * 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponent<NewBehaviourScript>().Damage(damage);
        }
    }

    private void Move()
    {
        r2d.AddForce(transform.right * speed);

        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, -checkPoint.up, 2f, 1 << 8);

        if (hit == false)
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    public void EnemyDam(float playdam)
    {
        enemyhp -= playdam;

        if (enemyhp <= 0)
        {
            Destroy(gameObject);
            npc.score.PlayerGet();
        }
    }
}
