using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Potal : MonoBehaviour
{
    public bool Player1Check;
    public bool Player2Check;
    public string SceneName;
    

    private void Start()
    {
        Player1Check = false;
        Player2Check = false;
    }

    private void Update()
    {
        
    }



    void ChangeScene()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "1-1":
                SceneManager.LoadScene("1-2");
                break;
            case "1-2":
                SceneManager.LoadScene("1-3");
                break;
            case "1-3":
                SceneManager.LoadScene("Chapter_Select");
                break;

        }
    }

    bool PlayerCheck()//플레이어 수 체크
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1-1":
            case "1-3":
                return true;

        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
            Player1Check = true;

        if(collision.gameObject.tag == "Player2")
            Player2Check = true;

        if(PlayerCheck()) // 플레이어가 한명일 때
        {
            if (Player1Check)
            {
                GameManager.instance.FadeOut();
                Invoke("ChangeScene", 1f);
            }
        }
        else  //플레이어가 두명일 때
        {
            if (Player1Check && Player2Check)
            {
                GameManager.instance.FadeOut();
                Invoke("ChangeScene", 1f);
            }
        }
        


    }
}
