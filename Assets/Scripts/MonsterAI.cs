using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed;

    public LayerMask whatIsGround;

    private RaycastHit2D wallDetection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = transform.right * speed;
    }

    public void ChangeDirection()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
