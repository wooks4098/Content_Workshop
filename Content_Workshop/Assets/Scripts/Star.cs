using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Boss boss;

    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player2")
        {
            boss.Damage();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            boss.Damage();
            gameObject.SetActive(false);
        }
    }
}
