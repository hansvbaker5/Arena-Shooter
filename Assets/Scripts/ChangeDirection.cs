using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    private MonsterAI parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<MonsterAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            parent.ChangeDirection();
        }
    }
}
