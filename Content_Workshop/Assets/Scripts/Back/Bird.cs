using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed;
    public Transform pos;

    private void Update()
    {
        gameObject.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bird_Box")
        {
            gameObject.transform.position = pos.position + new Vector3(-39, 0, 0);
        }
    }
}
