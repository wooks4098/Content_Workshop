using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public string Door_Color; //문 특성

    Animator anim;//애니메이션
    //Switch

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            anim.Play("Switch");
            GameObject.FindWithTag("Object").GetComponent<Door>().FadeIn();
        }
    }


}
