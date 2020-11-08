using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;//총알속도
    public float AppearTime;//총알 나타난 시간 체크

    private void Update()
    {
        AppearTime += Time.deltaTime;
        if(AppearTime>= 2)
            gameObject.SetActive(false);

    }
    public void Appear()
    {
        AppearTime = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Tile")
            gameObject.SetActive(false);
    }
   
}
