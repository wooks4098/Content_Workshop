using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Boss boss;
    public float time = 0;
    public float dissappeartime = 2f;
    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= dissappeartime)
            gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player2")
        {
            SoundManager.instance.SoundPlay("2Pitem");
            boss.Damage("2P");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            SoundManager.instance.SoundPlay("2Pitem");
            boss.Damage("2P");
            gameObject.SetActive(false);
        }
    }
}
