using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public string Switch_Color; //문 특성
    bool IsUse = false;
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
            if (IsUse)
                return;
            if(Switch_Color == "Red")
                anim.Play("Red_Switch");
            else if(Switch_Color == "Blue")
                anim.Play("Blue_Switch");
            GameObject Door_p = GameObject.Find("Door");
            for (int i = 0; i < Door_p.transform.childCount; i++)
            {
                if(Door_p.transform.GetChild(i).GetComponentInChildren<Door>().Dore_Color == Switch_Color)
                    Door_p.transform.GetChild(i).GetComponentInChildren<Door>().FadeIn();
            }
            IsUse = true;
        }
    }


}
